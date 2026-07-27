# NW.UnivariateForecasting.CLI

`NW.UnivariateForecasting.CLI` is a NuGet tool to perform univariate forecasting tasks on the values you provide. 

## Getting Started

If `/home/nw.univariateforecasting` is the folder in which the `.nupkg` file is located, you can install it and run it using these commands:

```cmd
dotnet tool install --global NW.UnivariateForecasting.CLI --add-source /home/nw.univariateforecasting
nwunif
```

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
nwunif session forecast --init:Init.json
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

If you wish to store the outcome to a handy JSON file, you can use the `--savesession` option:

```
nwunif session forecast --init:Init.json --savesession
```

The produced JSON will look like:

```json
{
  "Init": {
    "ObservationName": "Sales USD",
    "Values": [
      58.5,
      615.26,
      659.84,
      635.69,
      612.27,
      632.94
    ],
    "Coefficient": 0.5,
    "Error": 0.01,
    "Steps": 2
  },
  "Observations": [
    {
      "Coefficient": 0.5,
      "Error": 0.01,
      "NextValue": 316.48
    },
    {
      "Coefficient": 0.5,
      "Error": 0.01,
      "NextValue": 158.25
    }
  ],
  "Version": "4.2.1"
}
```

If you wish to store the files elsewhere, you can specify a new working folder by using the `--folderpath` option - i.e. `--folderpath:C:\Documents`.

## License
MIT