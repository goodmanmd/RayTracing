using CommandLine;

namespace InAWeekend
{
    public class Options
    {
        [Option('w', "width", Required = true, HelpText = "Image width")]
        public int Width { get; set; }

        [Option('h', "height", Required = true, HelpText = "Image height")]
        public int Height { get; set; }

        [Option('s', "samples", Required = false, HelpText = "Samples Per Pixel", Default = 10)]
        public int SamplesPerPixel { get; set; }

        [Option('d', "recurse", Required = false, HelpText = "Max recursion depth", Default = 50)]
        public int MaxRecurseDepth { get; set; }

        [Option('t', "threads", Required = false, HelpText = "Threads spawned for render. 0 = # of cores in the system", Default = 0)]
        public int MaxThreads { get; set; }

        [Option('f', "file", Required = false, HelpText = "Save output to a file", Default = false)]
        public bool SaveToFile { get; set; }

        [Option('g', "gui", Required = false, HelpText = "Display output image in a GUI window", Default = true)]
        public bool OutputToWindow { get; set; }

        [Option('p', "progress", Required = false, HelpText = "Display render progress to console", Default = true)]
        public bool ShowProgress { get; set; }

        public float AspectRatio => Width / (1.0f * Height);
    }
}