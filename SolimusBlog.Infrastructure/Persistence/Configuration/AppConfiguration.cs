using Microsoft.Extensions.Configuration;

namespace SolimusBlog.Infrastructure.Persistence.Configuration;

public static class AppConfiguration
{
    public static IConfigurationRoot LoadConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddUserSecrets("d7b6afee-9897-4d43-97ff-47a984c408ab")
            .Build();
    }
}