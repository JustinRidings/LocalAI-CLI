# LocalAI-CLI

![GitHub release (latest by date)](https://img.shields.io/github/v/release/JustinRidings/LocalAI-CLI)
![GitHub issues](https://img.shields.io/github/issues/JustinRidings/LocalAI-CLI)
![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/JustinRidings/LocalAI-CLI/release.yml?branch=main)
![GitHub](https://img.shields.io/github/license/JustinRidings/LocalAI-CLI)
![GitHub contributors](https://img.shields.io/github/contributors/JustinRidings/LocalAI-CLI)
![GitHub forks](https://img.shields.io/github/forks/JustinRidings/LocalAI-CLI)
![GitHub All Releases](https://img.shields.io/github/downloads/JustinRidings/LocalAI-CLI/total)

LocalAI-CLI is a command-line application that uses ONNX-based AI models for local text generation. This application initializes a local model and interacts with the user by generating text based on their input.

## Features

- Initializes a local ONNX AI model for text generation.
- Interacts with the user through a command-line interface.
- Uses environment variables to configure the text generation directory.
- Provides an option to set the environment variable if it is not already set.

## Requirements

- .NET 8.0 or later
- ONNX model for text generation

## Prereqs

You will need .NET 8 Runtime/SDK in order to code / build / use as a dotnet tool.

- [Install .NET 8 SDK/Runtime Interactively](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

OR use CommandLine

- `winget install dotnet-runtime-8`
- `winget install dotnet-sdk-8`

You will also need an [ONNX Text Generation Model](https://huggingface.co/models?pipeline_tag=text-generation&library=onnx&sort=trending) installed on your local machine. This demo was created using [Phi-3-mini-128k-intruct-onnx](https://huggingface.co/microsoft/Phi-3-mini-128k-instruct-onnx).

## Getting Started 

### 1. Clone the Repository

Clone the repository to your local machine:

```sh
git clone https://github.com/JustinRidings/LocalAI-CLI.git
cd .\LocalAI-CLI
```

### 2. Set the Model Directory Environment Variable

To save time between sessions, you can set a local environment variable for your Text Generation Model Directory. If this is unset, the application will ask you for it later. You'll want to confiigure this to point to the directory where the model itself lives. For example, using [Phi-3-mini-128k-intruct-onnx](https://huggingface.co/microsoft/Phi-3-mini-128k-instruct-onnx), you would set the directory to `C:\....\Phi-3-mini-128k-instruct-onnx\....\cpu-int4-rtn-block-32`

```sh
SETX LOCAL_ONNX_TEXTGEN_DIR C:\....\path\to\model /m
```

OR on Mac/Linux

```sh
export LOCAL_ONNX_TEXTGEN_DIR=/path/to/your/directory
```

### 3. Build and Run the Application

```sh
dotnet build
dotnet run
```

### 4. Example Interaction

```
----------------------
Welcome to LocalAI-CLI
----------------------
Please enter the directory path for text generation: /path/to/your/model
Environment variable LOCAL_ONNX_TEXTGEN_DIR is now set to: /path/to/your/model
Initializing local model. Please wait.
Loading Complete. Type anything and press Enter. Type 'X' and press Enter to exit.
You: Hello, AI!
AI: Hi there! How can I assist you today?
You: X
Exiting the application.
```
## Advanced Configurations

`OnnxTextGenerator` supports advanced configurations using different algorithms for token generation. By default, Greedy search is used. For more information about Advanced Configurations, please visit the [OnnxRuntimeGenAi](https://onnxruntime.ai/docs/genai/reference/config.html) documentation.

#### 1. Greedy Search (Default)
Greedy search is an approach where you make the best choice at each step, aiming for a quick and often good solution. Think of it like grabbing the biggest slice of pizza first, hoping it's the best move. It's fast and straightforward but doesn't always guarantee the best overall result.

```C#
    var defaultConfig = new OnnxConfig
    {
        TextModelDir = textGenDir,
        NumBeams = 1, // Default Value, doesn't need to be declared.
    };
```

### 2. Beam Search
Beam search is an approach where you explore several possibilities at once but only keep the top few at each step. Imagine you're trying to find the best path through a maze by exploring multiple routes but only continuing with the most promising ones. It’s efficient and often finds better solutions than just picking the best option each time.

```C#
    var beamSearchOnnxConfig = new OnnxConfig
    {
        TextModelDir = "your/model/dir",
        SearchType = OnnxConfig.OnnxSearchType.BeamSearch,
        NumBeams = 2 // NumBeams must be > 1
    };
```

### 3. TopN (Top P / Top K)
TopK is an approach that picks the top 'K' choices out of all possible options at each step. Imagine you're picking the best candies out of a bag, but you always pick the top 5 favorites each time. It helps in narrowing down choices while keeping the best options on the table. Top P (Nucleus) Search is an approach that considers options until a certain cumulative probability 'P' is reached. Think of it as choosing candies until the total sweetness hits a sweet spot. It’s more flexible and considers a wider variety of choices, ensuring some good surprises.

```C#
    var topNConfig = new OnnxConfig
    {
        TextModelDir = "your/model/dir",
        SearchType = OnnxConfig.OnnxSearchType.TopN,
        NumBeams = null, // Must be null to use TopN
        TopK = 1, // Used for Top K
        NucleusSampling = 0.75 // Used for Top P
    };
```
