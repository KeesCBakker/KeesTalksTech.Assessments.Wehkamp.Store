using System;

namespace KeesTalksTech.Assessments.Wehkamp.Store.WebApp.Helpers
{

    public class ComposerImplementationUpperCase: IComposer
    {
        public string Compose(string a, string b)
        {
            if(a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            
            return $"{a.ToUpper()}!!! {b.ToUpper()}!!!";
        }
    }
}