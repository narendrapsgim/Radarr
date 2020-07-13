using FluentValidation;
using NzbDrone.Core.Annotations;
using NzbDrone.Core.ThingiProvider;
using NzbDrone.Core.Validation;

namespace NzbDrone.Core.NetImport.RadarrList2.IMDbList
{
    public class IMDbSettingsValidator : AbstractValidator<IMDbListSettings>
    {
        public IMDbSettingsValidator()
        {
            RuleFor(c => c.ListId)
                .Matches(@"^top250$|^popular$|^ls\d+$")
                .WithMessage("List ID mist be 'top250', 'popular', or an IMDb List ID of the form 'lsxxxxxx");
        }
    }

    public class IMDbListSettings : IProviderConfig
    {
        private static readonly IMDbSettingsValidator Validator = new IMDbSettingsValidator();

        public IMDbListSettings()
        {
        }

        [FieldDefinition(1, Label = "List ID", HelpText = "IMDb list ID, 'top250' or 'popular'")]
        public string ListId { get; set; }

        public NzbDroneValidationResult Validate()
        {
            return new NzbDroneValidationResult(Validator.Validate(this));
        }
    }
}
