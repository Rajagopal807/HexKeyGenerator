# HexKeyGenerator

A .NET Framework 4.6 C# Windows application for generating random 256-bit hex keys associated with company names.

## Features

- Generate cryptographically secure 256-bit (32-byte) random hex keys
- Associate generated keys with company names
- User-friendly Windows Forms interface
- Store and manage key-company mappings
- Copy to clipboard functionality

## Requirements

- .NET Framework 4.6 or higher
- Windows OS
- Visual Studio 2015 or higher (for development)

## Getting Started

1. Clone the repository
2. Open `HexKeyGenerator.sln` in Visual Studio
3. Build the solution
4. Run the application

## Usage

1. Enter a company name in the input field
2. Click "Generate Key" to create a random 256-bit hex key
3. The generated 64-character hexadecimal key will be displayed
4. Click "Copy" to copy the key to your clipboard
5. View all company-key mappings in the list below
6. Use "Clear All" to reset all entries

## Technical Details

- **Cryptographic Security**: Uses `System.Security.Cryptography.RNGCryptoServiceProvider` for secure random number generation
- **Key Size**: 256-bit (32 bytes) keys
- **Format**: Hexadecimal representation (64 characters)
- **UI Framework**: Windows Forms
- **Language**: C#
- **Target Framework**: .NET Framework 4.6

## Project Structure

```
HexKeyGenerator/
├── HexKeyGenerator.sln          # Visual Studio Solution
├── HexKeyGenerator/
│   ├── HexKeyGenerator.csproj   # Project file
│   ├── Program.cs               # Application entry point
│   ├── MainForm.cs              # Windows Forms UI
│   ├── KeyGenerator.cs          # Key generation logic
│   ├── App.config               # Application configuration
│   └── Properties/
│       └── AssemblyInfo.cs      # Assembly metadata
├── .gitignore                   # Git ignore rules
└── README.md                    # This file
```

## Building

```bash
# Open in Visual Studio and Build (Ctrl+Shift+B)
# or use MSBuild from command line
msbuild HexKeyGenerator.sln /p:Configuration=Release
```

## License

MIT License

## Author

Rajagopal807
