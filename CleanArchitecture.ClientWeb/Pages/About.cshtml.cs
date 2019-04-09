using System.Reflection;
using System.Runtime.Versioning;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CleanArchitecture.ClientWeb.Pages
{
    public class AboutModel : PageModel
    {
        public string VersionInfo { get; set; }

        public void OnGet()
        {
            var framework = Assembly
                .GetEntryAssembly()?
                .GetCustomAttribute<TargetFrameworkAttribute>()?
                .FrameworkName;

            VersionInfo = framework;
        }
    }
}