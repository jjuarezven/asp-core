using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiPaises.Data;
using WebApiPaises.Models;

namespace WebApiPaises
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(ConfigureJson);
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("paisDB"));
        }

        private void ConfigureJson(MvcJsonOptions obj)
        {
            obj.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AppDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            if (!context.Paises.Any())
            {
                context.Paises.AddRange(new List<Pais>() {
                    new Pais {
                        Nombre = "Venezuela",
                        Estados = new List<Estado>() {
                            new Estado {
                                Nombre = "Miranda"
                            },
                            new Estado {
                                Nombre = "Guarico"
                            }
                        }
                    },
                    new Pais {
                        Nombre = "Colombia",
                        Estados = new List<Estado>() {
                            new Estado {
                                Nombre = "Antioquia"
                            },
                            new Estado {
                                Nombre = "Boyacá"
                            }
                        }
                    },
                    new Pais {
                        Nombre = "Peru",
                        Estados = new List<Estado>() {
                            new Estado {
                                Nombre = "Piura"
                            },
                            new Estado {
                                Nombre = "Apurimac"
                            }
                        }
                    },

                });
                context.SaveChanges();
            }
        }
    }
}
