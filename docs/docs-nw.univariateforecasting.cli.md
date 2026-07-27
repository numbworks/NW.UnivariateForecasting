# NW.UnivariateForecasting.CLI
Contact: numbworks@gmail.com

## Revision History

| Date | Author | Description |
|---|---|---|
| 2021-10-13 | numbworks | Created. |
| 2026-07-27 | numbworks | Last update (v4.2.1). |

## Introduction

`NW.UnivariateForecasting.CLI` is a command-line application built on the top of `NW.UnivariateForecasting` library.

## CLI Reference

|Command|Sub Command|Options|Exit Codes|
|---|---|---|---|
|session|||Success|
|session|forecast|--init:{filename}<br />*--folderpath:{path}*<br />*--savesession*<br />*--roundingdigits:{number}*<br />*--forecastingdenominator:{number}*|Success<br />Failure|

## Examples

The simplest command you can run is `session forecast`, which performs a forecasting task on the values you provide.

The command above requires that you have the required file (`Init.json`) located in the same folder as the application, which by default it's the working folder for all the application's activities. If you wish to store the files elsewhere, you can specify a new working folder by using the `folderpath` option - i.e. `--folderpath:C:\Documents`

At very least, the command will look like:

```
.\nwunif.exe session forecast --init:Init.json
```

The command above will log something like this to the console:

```
2023-03-09 10:31:21:013] Attempting to load a 'ForecastingInit' object from: C:\unifor\ForecastingInitBareMinimum.json.
[2023-03-09 10:31:21:105] A 'ForecastingInit' object has been successfully loaded.
[2023-03-09 10:31:21:107] Attempting to perform a forecasting task out of the provided ForecastingInit...
[2023-03-09 10:31:21:107] The provided folder path is: 'C:\unifor'.
[2023-03-09 10:31:21:107] The provided forecasting denominator is: '0.00000000000001'.
[2023-03-09 10:31:21:107] The provided rounding digits are: '2'.
[2023-03-09 10:31:21:108] The provided observation name is: ''.
[2023-03-09 10:31:21:108] The provided values are: '2'.
[2023-03-09 10:31:21:108] The provided coefficient is: 'null'.
[2023-03-09 10:31:21:108] The provided error is: 'null'.
[2023-03-09 10:31:21:108] The provided steps are: '1'.
[2023-03-09 10:31:21:108] Processing step nr.: '1'.
[2023-03-09 10:31:21:109] Creating a SlidingWindow out of the provided arguments...
[2023-03-09 10:31:21:109] The provided steps are: '2'.
[2023-03-09 10:31:21:115] The following SlidingWindow has been created: '[ Items: '2' ]
[ Id: '1', X_Actual: '58.5', Y_Forecasted: '615.26' ]
[ Id: '2', X_Actual: '615.26', Y_Forecasted: 'null' ]'.
[2023-03-09 10:31:21:117] Creating an Observation out of the provided SlidingWindow: '[ Items: '2' ]'...
[2023-03-09 10:31:21:125] The following Observation has been created: '[ Coefficient: '0.1', Error: '0', NextValue: '61.53' ]'.
[2023-03-09 10:31:21:125] The observation's coefficient is: '0.1'.
[2023-03-09 10:31:21:125] The observation's error is: '0'.
[2023-03-09 10:31:21:126] The current observation's next value is: '61.53'.
[2023-03-09 10:31:21:126] The forecasting task has been successfully completed.
[2023-03-09 10:31:21:131] Attempting to save the provided 'ForecastingSession' object as: C:\unifor\unifor_session_20230309113121126.json.
[2023-03-09 10:31:21:156] The provided 'ForecastingSession' object has been successfully saved.
```


## Markdown Toolset

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)