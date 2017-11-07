using System;

namespace KeesTalksTech.Assessments.Wehkamp.Store.WebApp.Helpers
{

    public class ComposerImplementation : UriBuilder, IComposer
    {
        public string Compose(string a, string b)
        {
            return $"{a}/{b}";
        }
    }
}