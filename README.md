# LocalAI-CLI

LocalAI-CLI is a command-line application that uses ONNX-based AI models for local text generation. This application initializes a local model and interacts with the user by generating text based on their input.

## Features

- Initializes a local ONNX AI model for text generation.
- Interacts with the user through a command-line interface.
- Uses environment variables to configure the text generation directory.
- Provides an option to set the environment variable if it is not already set.

## Requirements

- .NET 8.0 or later
- ONNX model for text generation

## Getting Started

### Prereqs

You will need .NET 8 Runtime/SDK in order to code / build / use as a dotnet tool.

- [Install .NET 8 SDK/Runtime Interactively](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

OR use CommandLine

- `winget install dotnet-runtime-8`
- `winget install dotnet-sdk-8`

You will also need an [ONNX Text Generation Model](https://huggingface.co/models?pipeline_tag=text-generation&library=onnx&sort=trending) installed on your local machine. This demo was created using [Phi-3-mini-128k-intruct-onnx](https://huggingface.co/microsoft/Phi-3-mini-128k-instruct-onnx).

### 1. Clone the Repository

Clone the repository to your local machine:

```sh
git clone https://github.com/JustinRidings/LocalAI-CLI.git
cd .\LocalAI-CLI
```

### 2. Set the Model Directory Environment Variable

To save time between sessions, you can set a local environment variable for your Text Generation Model Directory. If this is unset, the application will ask you for it later.

```sh
set LOCAL_ONNX_TEXTGEN_DIR=C:\path\to\your\directory
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
