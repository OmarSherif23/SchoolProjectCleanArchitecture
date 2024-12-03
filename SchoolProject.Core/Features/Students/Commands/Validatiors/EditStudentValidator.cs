using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

using System.Threading.Tasks;
using SchoolProject.Core.Features.Students.Commands.Models;


namespace SchoolProject.Core.Features.Students.Commands.Validatiors
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        #endregion

        #region Constructors
        public EditStudentValidator(IStudentService studentService)
        {
            _studentService = studentService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage("Name must not be Empty")
                .NotNull().WithMessage("Name must not be Empty")
                .MaximumLength(100).WithMessage("Max Length is 100");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address must not be Empty")
                .NotNull().WithMessage("Address must not be Empty")
                .MaximumLength(100).WithMessage("{PropertyName} Length is 100");
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.NameEn).MustAsync(async (model,key, CancellationToken) => !await _studentService.IsNameEnExistExcludeSelf(key,model.Id)).WithMessage("Name is Exist");
            RuleFor(x => x.NameAr).MustAsync(async (model,key, CancellationToken) => !await _studentService.IsNameArExistExcludeSelf(key,model.Id)).WithMessage("Name is Exist");
        }
        #endregion
    }   
}
