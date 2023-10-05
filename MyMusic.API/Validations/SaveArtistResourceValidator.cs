using FluentValidation;
using MyMusic.API.Resourses;

namespace MyMusic.API.Validations
{
    public class SaveArtistResourceValidator : AbstractValidator<SaveArtistResource>
    {

        public SaveArtistResourceValidator() 
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
