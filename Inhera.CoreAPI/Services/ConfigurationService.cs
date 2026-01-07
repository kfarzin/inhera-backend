using Inhera.Shared.Enums;
using Inhera.Shared.VMs.Common;

namespace Inhera.CoreAPI.Services
{
    public class ConfigurationService
    {
        public ConfigurationService()
        {
        }

        public List<KeyValueVm> GetGenders(string lang = "en")
        {
            var genders = Enum.GetValues<GenderTypes>();
            var result = new List<KeyValueVm>();

            foreach (var gender in genders)
            {
                result.Add(new KeyValueVm
                {
                    Key = gender.ToString(),
                    Value = GetGenderTranslation(gender, lang)
                });
            }

            return result;
        }

        private string GetGenderTranslation(GenderTypes gender, string lang)
        {
            // Default to English if language not supported
            return lang.ToLower() switch
            {
                "en" => gender switch
                {
                    GenderTypes.Male => "Male",
                    GenderTypes.Female => "Female",
                    GenderTypes.Other => "Other",
                    _ => gender.ToString()
                },
                "de" => gender switch
                {
                    GenderTypes.Male => "Männlich",
                    GenderTypes.Female => "Weiblich",
                    GenderTypes.Other => "Andere",
                    _ => gender.ToString()
                },
                "fr" => gender switch
                {
                    GenderTypes.Male => "Homme",
                    GenderTypes.Female => "Femme",
                    GenderTypes.Other => "Autre",
                    _ => gender.ToString()
                },
                _ => gender.ToString() // Fallback to enum name
            };
        }
    }
}
