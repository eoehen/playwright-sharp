using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PlaywrightSharp.Helpers;
using PlaywrightSharp.Tests.Attributes;
using PlaywrightSharp.Tests.BaseTests;
using Xunit;
using Xunit.Abstractions;

namespace PlaywrightSharp.Tests
{
    ///<playwright-file>screencast.spec.js</playwright-file>
    [Collection(TestConstants.TestFixtureBrowserCollectionName)]
    public class ScreencastTests : PlaywrightSharpBrowserBaseTest
    {
        /// <inheritdoc/>
        public ScreencastTests(ITestOutputHelper output) : base(output)
        {
        }

        ///<playwright-file>screencast.spec.js</playwright-file>     
        ///<playwright-it>videoSize should require videosPath</playwright-it>
        [SkipBrowserAndPlatformFact(skipWebkit: true, skipWindows: true)]
        public async Task VideoSizeShouldRequireVideosPath()
        {
            var exception = await Assert.ThrowsAnyAsync<PlaywrightSharpException>(() => Browser.NewContextAsync(videoSize: new ViewportSize { Width = 100, Height = 100 }));
            Assert.Contains("\"videoSize\" option requires \"videosPath\" to be specified", exception.Message);
        }

        ///<playwright-file>screencast.spec.js</playwright-file>     
        ///<playwright-it>should capture static page</playwright-it>
        [SkipBrowserAndPlatformFact(skipWebkit: true, skipWindows: true)]
        public async Task ShouldCaptureStaticPage()
        {
            using var tempDirectory = new TempDirectory();
            var context = await Browser.NewContextAsync(
                videosPath: tempDirectory.Path,
                videoSize: new ViewportSize { Width = 100, Height = 100 });

            var page = await context.NewPageAsync();
            await page.EvaluateAsync("() => document.body.style.backgroundColor = 'red'");
            await Task.Delay(1000);
            await context.CloseAsync();

            Assert.NotEmpty(new DirectoryInfo(tempDirectory.Path).GetFiles("*.webm"));
        }

        ///<playwright-file>screencast.spec.js</playwright-file>     
        ///<playwright-it>should capture navigation</playwright-it>
        [Fact(Skip = "We don't need to test video details")]
        public void ShouldCaptureNavigation()
        {
        }

        ///<playwright-file>screencast.spec.js</playwright-file>     
        ///<playwright-it>should capture css transformation</playwright-it>
        [Fact(Skip = "We don't need to test video details")]
        public void ShouldCaptureCssTransformation()
        {
        }

        ///<playwright-file>screencast.spec.js</playwright-file>     
        ///<playwright-it>should work for popups</playwright-it>
        [Fact(Skip = "We don't need to test video details")]
        public void ShouldWorkForPopups()
        {
        }

        ///<playwright-file>screencast.spec.js</playwright-file>     
        ///<playwright-it>should scale frames down to the requested size</playwright-it>
        [Fact(Skip = "We don't need to test video details")]
        public void ShouldScaleFramesDownToTheRequestedSize()
        {
        }

        ///<playwright-file>screencast.spec.js</playwright-file>     
        ///<playwright-it>should use viewport as default size</playwright-it>
        [Fact(Skip = "We don't need to test video details")]
        public void ShouldUseViewportAsDefaultSize()
        {
        }

        ///<playwright-file>screencast.spec.js</playwright-file>     
        ///<playwright-it>should be 1280x720 by default</playwright-it>
        [Fact(Skip = "We don't need to test video details")]
        public void ShouldBe1280x720ByDefault()
        {
        }

        ///<playwright-file>screencast.spec.js</playwright-file>     
        ///<playwright-it>should capture static page in persistent context</playwright-it>
        [SkipBrowserAndPlatformFact(skipWebkit: true, skipWindows: true)]
        public async Task ShouldCaptureStaticPageInPersistentContext()
        {
            using var userDirectory = new TempDirectory();
            using var tempDirectory = new TempDirectory();
            var context = await BrowserType.LaunchPersistentContextAsync(
                userDirectory.Path,
                videosPath: tempDirectory.Path,
                videoSize: new ViewportSize { Width = 100, Height = 100 });

            var page = await context.NewPageAsync();
            await page.EvaluateAsync("() => document.body.style.backgroundColor = 'red'");
            await Task.Delay(1000);
            await context.CloseAsync();

            Assert.NotEmpty(new DirectoryInfo(tempDirectory.Path).GetFiles("*.webm"));
        }
    }
}
