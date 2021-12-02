using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LojaVirtual.Libraries.Validacao
{
    public class EmailUnicoColaboradorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext )
        {
            var email = (value as string).Trim();

            IRepositoryColaborador repositoryColaborador = (IRepositoryColaborador)validationContext.GetService(typeof(IRepositoryColaborador));

            var colaboradores = repositoryColaborador.ObterPorEmail(email);

            var colaboradorObj = (Colaborador)validationContext.ObjectInstance;

            if (colaboradores.Count > 1)
            {
                return new ValidationResult("E-mail já existe");
            }

            if (colaboradores.Count == 1 && colaboradorObj.Id != colaboradores[0].Id)
            {
                return new ValidationResult("E-mail já existe");
            }

            return ValidationResult.Success;
        }
    }
}
