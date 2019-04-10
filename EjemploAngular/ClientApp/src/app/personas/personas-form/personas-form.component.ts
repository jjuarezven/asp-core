import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PersonasService } from '../personas.service';
import { IPersona } from 'src/app/models/persona';
import { Router, ActivatedRoute } from '@angular/router';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-personas-form',
  templateUrl: './personas-form.component.html',
  styleUrls: ['./personas-form.component.css']
})
export class PersonasFormComponent implements OnInit {
  formGroup: FormGroup;
  modoEdicion: boolean = false;
  personaId: number;

  constructor(private fb: FormBuilder,
    private personasService: PersonasService,
    private router: Router,
    private activatedRouter: ActivatedRoute) { }

  ngOnInit() {
    this.formGroup = this.fb.group({
      nombre: '',
      fechaNacimiento: ''
    });

    this.activatedRouter.params.subscribe(params => {
      if (params["id"] == undefined) {
        return;
      }
      this.modoEdicion = true;
      this.personaId = params["id"];
      this.personasService.getPersona(this.personaId.toString()).subscribe(persona => this.cargarFormulario(persona), error => this.router.navigate(["/personas"]));
    });
  }
  cargarFormulario(persona: IPersona): void {
    var dp = new DatePipe(navigator.language);
    var format = "yyyy-MM-dd";
    this.formGroup.patchValue({
      nombre: persona.nombre,
      fechaNacimiento: dp.transform(persona.fechaNacimiento, format)
    });
  }

  save() {
    const persona: IPersona = Object.assign({}, this.formGroup.value);
    if (this.modoEdicion) {
      persona.id = this.personaId;
      this.personasService.updatePersona(persona).subscribe(() => this.onSaveSuccess(), error => console.log(error));
    } else {
      this.personasService.createPersona(persona).subscribe(() => this.onSaveSuccess(), error => console.log(error));
    }

  }

  onSaveSuccess(): void {
    this.router.navigate(["/personas"]);
  }

}
