using System;

namespace InyeccionDependencias.Services
{
    public interface IOperation
    {
        Guid OperationId { get; }
    }

    public class Operation : IOperationScoped, IOperationSingleton, IOperationSingletonInstance, IOperationTransient
    {
        private readonly Guid guid;

        public Operation()
        {
            this.guid = Guid.NewGuid();
        }

        public Operation(Guid guid)
        {
            this.guid = guid;
        }

        public Guid OperationId { get { return guid; } }
    }

    public interface IOperationTransient : IOperation
    {
    }

    public interface IOperationSingletonInstance : IOperation
    {
    }

    public interface IOperationSingleton : IOperation
    {
    }

    public interface IOperationScoped : IOperation
    {
    }
}
