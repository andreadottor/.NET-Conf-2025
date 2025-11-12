namespace XE.DotNetConf2025.ApiService.Endpoints
{
    using Microsoft.AspNetCore.Http.HttpResults;
    using XE.DotNetConf2025.Models;

    /// <summary>
    /// Provides extension methods for mapping endpoints related to speeches in an ASP.NET Core application.
    /// </summary>
    /// <remarks>This static class defines methods to register HTTP endpoints for retrieving and inserting
    /// speech data. Use these methods to configure routes for speech-related operations when setting up your
    /// application's endpoint routing.</remarks>
    public static class SpeechesEnpoints
    {
        public static IEndpointRouteBuilder MapSpeechesEnpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/speeches", GetSpeeches);
            routes.MapPost("/speeches", InsertSpeech);
            return routes;
        }

        /// <summary>
        /// Returns an HTTP 200 OK response containing a collection of available speeches.
        /// </summary>
        /// <returns>An <see cref="Ok{T}"/> result containing an enumerable collection of <see cref="Speech"/> objects
        /// representing the available speeches.</returns>
        public static Ok<IEnumerable<Speech>> GetSpeeches()
        {
            return TypedResults.Ok<IEnumerable<Speech>>(new List<Speech>
                {
                    new Models.Speech
                    {
                        Title = ".NET Conf 2025 is awesome",
                        Abstract = "Learn about all the new features in .NET 10.0",
                        Speaker = new Models.Speaker
                        {
                            Name = "Jane Doe",
                            Email = "jane.doe@xedotnet.org"
                        }
                    },
                    new Models.Speech
                    {
                        Title = "Building Cloud-Native Apps with .NET 10.0",
                        Abstract = "Explore the latest cloud-native features in .NET 10.0",
                        Speaker = new Models.Speaker
                        {
                            Name = "John Smith",
                            Email = "john.smith@xedotnet.org"
                        }
                    }
                });
        }

        /// <summary>
        /// Creates a new speech resource and returns a result indicating that the resource has been created.
        /// </summary>
        /// <param name="speech">The speech object to be created. Cannot be null.</param>
        /// <returns>A Created result containing the newly created speech resource and its location URI.</returns>
        public static Created<Speech> InsertSpeech(Speech speech)
        {
            // In a real application, you would save the speech to a database here.
            // For this example, we just return the speech with a Created result.
            return TypedResults.Created($"/speeches/{Guid.NewGuid()}", speech);
        }
    }
}
