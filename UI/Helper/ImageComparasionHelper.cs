using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace EasternPeakAutomation.UI.Helper
{
    /// <summary>
    /// Provides methods for generating screenshot file paths and comparing images at the pixel level.
    /// </summary>
    public static class ImageComparisonHelper
    {
        /// <summary>
        /// Retrieves the file paths for baseline and current screenshots for a given menu item.
        /// </summary>
        /// <param name="menuItem">The menu item identifier used for naming the screenshots.</param>
        /// <returns>
        /// A tuple containing the baseline image path and the current image path.
        /// </returns>
        public static (string baselinePath, string currentPath) GetScreenshotPaths(string menuItem)
        {
            // Determine the project root directory based on the test context.
            string projectRoot = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, "..", "..", ".."));
            string imagesFolder = Path.Combine(projectRoot, "UI", "Images");
            string baselineFolder = Path.Combine(imagesFolder, "baseline");
            string currentFolder = Path.Combine(imagesFolder, "current");
            
            // Create the baseline and current directories if they don't exist.
            if (!Directory.Exists(baselineFolder))
                Directory.CreateDirectory(baselineFolder);
            if (!Directory.Exists(currentFolder))
                Directory.CreateDirectory(currentFolder);
            
            // Construct file paths for the screenshots.
            string baselinePath = Path.Combine(baselineFolder, $"{menuItem}_baseline.png");
            string currentPath = Path.Combine(currentFolder, $"{menuItem}_current.png");

            Log.Information($"Screenshot paths determined: Baseline = {baselinePath}, Current = {currentPath}");
            return (baselinePath, currentPath);
        }
        
        /// <summary>
        /// Compares two images on a pixel-by-pixel basis and calculates the difference percentage.
        /// </summary>
        /// <param name="baselinePath">The file path of the baseline image.</param>
        /// <param name="currentPath">The file path of the current image.</param>
        /// <param name="tolerance">
        /// The acceptable difference ratio (between 0 and 1). For example, 0.01 represents a 1% difference tolerance.
        /// </param>
        /// <returns>
        /// True if the images differ by less than or equal to the specified tolerance; otherwise, false.
        /// </returns>
        public static bool CompareImages(string baselinePath, string currentPath, double tolerance = 0.01)
        {
            // Check if baseline image exists. If not, create it from the current image.
            if (!File.Exists(baselinePath))
            {
                Log.Warning("Baseline image not found at {BaselinePath}. Creating baseline image from current screenshot.", baselinePath);
                // Create a copy of the current screenshot as the baseline.
                File.Copy(currentPath, baselinePath);
            }
            
            using (Image<Rgba32> baseline = Image.Load<Rgba32>(baselinePath))
            using (Image<Rgba32> current = Image.Load<Rgba32>(currentPath))
            {
                // Verify that image dimensions match.
                if (baseline.Width != current.Width || baseline.Height != current.Height)
                {
                    Log.Warning("Image dimensions do not match. Comparison aborted.");
                    return false;
                }
                
                long totalDiff = 0;
                // Loop through each pixel to compute the cumulative difference.
                for (int y = 0; y < baseline.Height; y++)
                {
                    for (int x = 0; x < baseline.Width; x++)
                    {
                        Rgba32 bPixel = baseline[x, y];
                        Rgba32 cPixel = current[x, y];
                        totalDiff += Math.Abs(bPixel.R - cPixel.R);
                        totalDiff += Math.Abs(bPixel.G - cPixel.G);
                        totalDiff += Math.Abs(bPixel.B - cPixel.B);
                    }
                }
                // Calculate the maximum possible difference.
                double maxDiff = baseline.Width * baseline.Height * 3 * 255.0;
                double diffPercentage = totalDiff / maxDiff;
                Log.Information($"Calculated difference percentage: {diffPercentage:P2}");
                
                bool match = diffPercentage <= tolerance;
                Log.Information(match 
                    ? "Images match within the acceptable tolerance." 
                    : "Images do not match within the acceptable tolerance.");
                return match;
            }
        }
    }
}