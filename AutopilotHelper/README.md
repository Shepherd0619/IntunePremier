# AutopilotHelper

**AutopilotHelper** is a C#-based tool designed to provide an easy-to-use, graphical interface for MDM log analysis and Autopilot monitoring. It aims to streamline IT operations by offering a comprehensive solution that simplifies the process of managing and analyzing logs from multiple sources.

## Features

- **Visualize MDM Logs**: A user-friendly interface for reviewing MDM logs.
- **Faster Event Viewer**: Quickly search and filter evtx logs to identify issues.
- **Visualize MDM Registry**: Make it easier to identify enrollment status, policy manager, etc just like how you do with local machine's Registry Editor.
- **Visualize ESP Stage**: Easy to know which stage of ESP is failing.
- **Ease of Use**: Extract, analyze, and visualize data without any setup required.

## Requirements

- .NET 6.0
- Windows ADK (for autopilot hardware hash)
- Visual Studio 2022 (optional for development)

## Installation

1. Download the latest release from **Release** section.
2. Extract the downloaded archive.
3. Run `AutopilotHelper.exe`.

## Usage

1. Launch `AutopilotHelper`.
2. Select the cab or zip generated from `mdmdiagnosticstool.exe`.
3. Start analyzing logs, and use the tool provided to help your troubleshooting.

## Contributing

Currently we only accept bug fixes and minor improvements. If you would like to contribute, please fork the repository and submit a pull request.

## License

This project is licensed under the BSD 3-Clause License.

---

For any issues or questions, please feel free to open an issue in this repository.
