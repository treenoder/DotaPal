using System.Collections.Generic;

namespace DotaPal
{
    public class I18N
    {
        private Language _language;
        private Dictionary<Language, Dictionary<string, string>> _dictionary = new();

        public I18N(Language language)
        {
            _language = language;
            _dictionary[Language.Ru] = new Dictionary<string, string>();
            _dictionary[Language.En] = new Dictionary<string, string>();
            _dictionary[Language.Ru][Action.AddCooldown.ToString()] = "Добавить время на таймер";
            _dictionary[Language.En][Action.AddCooldown.ToString()] = "Add time to a timer";
            _dictionary[Language.Ru][Action.ResetCooldown.ToString()] = "Сбросить таймер";
            _dictionary[Language.En][Action.ResetCooldown.ToString()] = "Reset a timer";
            _dictionary[Language.Ru][Action.ChangeSide.ToString()] = "Сменить сторону таймеров сверху";
            _dictionary[Language.En][Action.ChangeSide.ToString()] = "Switch top timers side";
            _dictionary[Language.Ru][Action.ToggleOverlay.ToString()] = "Включить/Выключить интерфейс";
            _dictionary[Language.En][Action.ToggleOverlay.ToString()] = "Show/Hide overlay";
            _dictionary[Language.Ru]["Hero"] = "Герой";
            _dictionary[Language.En]["Hero"] = "Hero";
            _dictionary[Language.Ru]["Extra"] = "Дополнительный";
            _dictionary[Language.En]["Extra"] = "Extra";
            _dictionary[Language.Ru]["LMenu"] = "Alt";
            _dictionary[Language.En]["LMenu"] = "Alt";
            _dictionary[Language.Ru]["LControlKey"] = "Ctr";
            _dictionary[Language.En]["LControlKey"] = "Ctr";
        }

        public string Translate(string key)
        {
            return _dictionary[_language][key];
        }

        public Language GetLanguage()
        {
            return _language;
        }

        public void SetLanguage(Language lang)
        {
            _language = lang;
        }
    }
}