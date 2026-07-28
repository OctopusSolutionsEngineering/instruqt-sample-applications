
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenFeature;
using Octopus.OpenFeature;
using System.Runtime.CompilerServices;
using Octopus.OpenFeature.Provider;

public class ItemsModel : PageModel
{
    /*
    public void OnGet()
    {
        // JS handles data fetching
    }
    */

    // Private variables
    private readonly IFeatureClient _featureClient;
    private readonly IConfiguration _configuration;
    private string _clientIdentifier = string.Empty;
    private string _flagSlug = string.Empty;

    // Public variables
    public bool isFeatureEnabled { get; private set; } = false;
    public string flagSlug => _flagSlug;

    public ItemsModel(IFeatureClient featureClient, IConfiguration configuration)
    {
        _featureClient = featureClient;
        _configuration = configuration;
        _clientIdentifier = _configuration.GetValue<string>("FeatureFlags:ClientIdentifier") ?? string.Empty;
        _flagSlug = _configuration.GetValue<string>("FeatureFlags:Slug") ?? string.Empty;
    }

    public async Task OnGetAsync()
    {
        // Check to make sure both the client identifier and flag name are set
        if (string.IsNullOrEmpty(_clientIdentifier) == false && string.IsNullOrEmpty(_flagSlug) == false)
        {
            // Init provider
            var provider = new OctopusFeatureProvider(new OctopusFeatureConfiguration(_clientIdentifier, new ProductMetadata("OctopusSample")));
            await OpenFeature.Api.Instance.SetProviderAsync(provider);
            var client = OpenFeature.Api.Instance.GetClient();

            // Make call to get the feature flag value
            this.isFeatureEnabled = await client.GetBooleanValueAsync(_flagSlug, false);
        }
    }
}
