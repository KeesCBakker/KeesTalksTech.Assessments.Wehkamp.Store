using System;
using System.Linq;


namespace KeesTalksTech.Assessments.Wehkamp.Store.WebApp.Helpers
{
    public class MyComponent
    {
        private readonly IComposer composer;

        public MyComponent(IComposer composer)
        {
            this.composer = composer ?? throw new ArgumentNullException(nameof(composer));
        }

        public string SaySomething(params string[] something)
        {
            var phrase = this.GetType().Name;

            phrase = phrase + " uses " + composer.GetType().Name + ":";

            //compose things
            something.ToList().ForEach(s => phrase = composer.Compose(phrase, s));

            return phrase;
        }
    }
}
