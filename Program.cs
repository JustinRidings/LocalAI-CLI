using Microsoft.KernelMemory;
using Microsoft.KernelMemory.AI.OpenAI;
using Microsoft.KernelMemory.AI.Onnx;
using Microsoft.KernelMemory.AI;

#pragma warning disable KMEXP01 // OnnxTextGenerator is still in evaluation

Console.WriteLine("----------------------");
Console.WriteLine("Welcome to LocalAI-CLI");
Console.WriteLine("----------------------");

var textGenDir = InitializeEnv();

if(!string.IsNullOrEmpty(textGenDir))
{
    var basicOnnxConfig = new OnnxConfig
    {
        TextModelDir = textGenDir
    };

    Console.WriteLine("Initializing local model. Please wait.");

    var onnxTextGen = new OnnxTextGenerator(basicOnnxConfig, textTokenizer: new GPT4oTokenizer());

    Console.WriteLine("Loading Complete. Type anything and press Enter. Type 'X' and press Enter to exit.");

    while (true)
    {
        Console.Write("You: ");
        string? input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input) && input.ToUpper() == "X")
        {
            Console.WriteLine("Exiting the application.");
            break;
        }

        var sysPrompt = "Reply to the following prompt briefly and concisely. Avoid over-sharing.";

        // The prompt template you use is entirely dependent on the Model you're using.
        // This example was created using Phi-3-mini-128k-instruct-onnx\cpu_and_mobile\cpu-int4-rtn-block-32
        var promptTemplate = $"<|system|>{sysPrompt}<|end|><|user|>{input}<|end|><|assistant|>";
        var options = new TextGenerationOptions();

        Console.Write("AI: ");
        await foreach (string token in onnxTextGen.GenerateTextAsync(promptTemplate, options))
        {
            Console.Write(token);
        }
        Console.WriteLine();
    }
}

string InitializeEnv()
{
    string? textGenDir = Environment.GetEnvironmentVariable("LOCAL_ONNX_TEXTGEN_DIR");

    if (string.IsNullOrEmpty(textGenDir))
    {
        Console.WriteLine("Environment variable LOCAL_ONNX_TEXTGEN_DIR is not set.");
        Console.Write("Please enter the directory path for text generation: ");
        textGenDir = Console.ReadLine();

        if (!string.IsNullOrEmpty(textGenDir))
        {
            Environment.SetEnvironmentVariable("LOCAL_ONNX_TEXTGEN_DIR", textGenDir);
            Console.WriteLine($"Environment variable LOCAL_ONNX_TEXTGEN_DIR is now set to: {textGenDir}");
            return textGenDir;
        }
        else
        {
            Console.WriteLine("No directory path provided. Exiting the application.");
            return string.Empty;
        }
    }
    else
    {
        Console.WriteLine($"Using text generation directory: {textGenDir}");
        return textGenDir;
    }

}