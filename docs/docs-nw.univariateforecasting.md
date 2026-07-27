# NW.UnivariateForecasting
Contact: numbworks@gmail.com

## Revision History

| Date | Author | Description |
|---|---|---|
| 2020-04-27 | numbworks | Created. |
| 2020-11-28 | numbworks | Added new examples, re-organized the document. |
| 2020-12-04 | numbworks | Added examples of user-provided C and E. |
| 2020-12-06 | numbworks | Added "Saving and Loading" paragraph. |
| 2020-12-22 | numbworks | Changed font size and date format in Revision History. |
| 2021-10-11 | numbworks | Version numbers removed. |
| 2023-03-09 | numbworks | Updated to v3.0.0. |
| 2024-02-08 | numbworks | Updated to v4.0.0. |

## Introduction

`NW.UnivariateForecasting` is a library to perform univariate forecasting tasks on the values you provide. 

## What does "univariate forecasting" mean?

*Time Series Forecasting* is a *machine learning* technique that aims to predict the next values in a time series when a subset of subsequent timestamped values is provided ("*sliding window*"). There is no other information available than the timestamp and the value itself.

For example, given the last six months of "*Total Monthly Sales USD*" of your company, you would like the machine to predict the amounts for the next x months. 

*Time Series Forecasting* is divided in *Univariate* and *Multivariate*. 

The first one can predict only one step ahead, while the second one can predict multiple steps ahead. As its name states, this library implements the univariate approach.

A good definition of "*univariate*" could be: "*[...] univariate refers to an expression, equation, function or polynomial of only one variable [...] which consists of observations on only a single characteristic or attribute.*"

## Getting Started

In order to use the library, the first thing we need to do is to create a `ForecastingInit` object containing the initialization data and then pass it to the `Forecast` method of the a `UnivariateForecaster` object, as shown in the following example:

```csharp
using System;
using System.Collections.Generic;
using NW.UnivariateForecasting;
using NW.UnivariateForecasting.Forecasts;

/* ... */

ForecastingInit init
    = new ForecastingInit(
            values: new List<double>() { 58.5, 615.26 },
            steps: 1
            observationName: null,
            coefficient: null,
            error: null
        );

IUnivariateForecaster forecaster = new UnivariateForecaster();
ForecastingSession session = forecaster.Forecast(init);

/* ... */
```

Highlights:

- `values` must contains at least two items of a time serie of whatever kind;
- `steps` refers to the number of `steps` (how many values you want to forecast) and it must be at least one (obviously);
- if provided, `coefficient` and `error` will overwrite the ones calculated by the library;
- `observationName` is a (optional) label about what `values` refer to 

The output of the forecasting task will be a `ForecastingSession` object, which would look like the example below if instantiated manually:

```csharp
ForecastingSession session 
    = new ForecastingSession(
        init: new ForecastingInit(
                values: new List<double>() { 58.5, 615.26 },
                steps: 1
                observationName: null,
                coefficient: null,
                error: null
            ),
        observations: new List<Observation>() {
                new Observation(
                    coefficient: 0.095081754055196, 
                    error: 0, 
                    nextValue: 58.49999999999989
            )};
        version: "4.0.0.0"
    );
```

By default, the amounts calculated by the library (`coefficient`, `error`, ...) are rounded to the 15th decimal digit (the maximum amount of digits for the `double` amounts), but you might want to customize this aspect to improve readibility. 

If so, you can inject a custom `SettingBag` object into the `UnivariateForecaster` object, as shown in the following example:

```csharp
using System;
using System.Collections.Generic;
using NW.UnivariateForecasting;
using NW.UnivariateForecasting.Bags;
using NW.UnivariateForecasting.Forecasts;

/* ... */

ForecastingInit init
    = new ForecastingInit(
            values: new List<double>() { 58.5, 615.26 },
            steps: 1
            observationName: null,
            coefficient: null,
            error: null
        );

SettingBag settingBag = new SettingBag(
    forecastingDenominator: SettingBag.DefaultForecastingDenominator,
    folderPath: SettingBag.DefaultFolderPath,
    roundingDigits: 2
);

IUnivariateForecaster forecaster = new UnivariateForecaster(
    componentBag: new ComponentBag(),
    settingBag: settingBag
);
ForecastingSession session = forecaster.Forecast(init);

/* ... */
```

This code will output the following `ForecastingSession` object:

```csharp
ForecastingSession session 
    = new ForecastingSession(
        init: new ForecastingInit(
                values: new List<double>() { 58.5, 615.26 },
                steps: 1
                observationName: null,
                coefficient: null,
                error: null
            ),
        observations: new List<Observation>() {
                new Observation(
                    coefficient: 0.1, 
                    error: 0, 
                    nextValue: 61.53
            )};
        version: "4.0.0.0"
    );
```

The user will only interact with `ForecastingInit` and `ForecastingSession` objects, but the library will use a data structure known as `SlidingWindow` to internally organize and transfer data:

```csharp
SlidingWindow slidingWindow = new SlidingWindow(

    items: new List<SlidingWindowItem>() {
            new SlidingWindowItem(id: 1, X_Actual: 58.5, Y_Forecasted: 615.26),
            new SlidingWindowItem(id: 2, X_Actual: 615.26, Y_Forecasted: 659.84),
            new SlidingWindowItem(id: 3, X_Actual: 659.84, Y_Forecasted: 635.69),
            new SlidingWindowItem(id: 4, X_Actual: 635.69, Y_Forecasted: 612.27),
            new SlidingWindowItem(id: 5, X_Actual: 612.27, Y_Forecasted: 632.94),
            new SlidingWindowItem(id: 6, X_Actual: 632.94, Y_Forecasted: null)

});
```

The `SlidingWindow` object above corresponds to the following list of values:

```csharp
List<double> values 
    = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();
```

Each `SlidingWindowItem` object has the following properties:

- `Id` is a sequential identifier (a number works fine in this case)
- `X_Actual` is the current amount
- `Y_Forecasted` is the next amount in the time series

The `Y_Forecasted` for the last `*Item` is `[NULL]` and it's the value we want to predict.

Once the `SlidingWindow` object is set, we are ready to perform the prediction itself, which will return an `Observation` object:

```csharp
Observation observation 
    = new Observation(coefficient: 0.82, error: 0.22, nextValue: 519.23);
```
The original time series was: `{ 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }`.
According to the univariate forecasting, the next value of the series will be: `519.23`.

## Load and save

The library is able to load and save different key-objects using JSON format. 

Here an example of each JSON file produced by the library:

1. [ForecastingInitBareMinimum.json](ExampleFiles/ForecastingInitBareMinimum.json)
2. [ForecastingInitSingleWithCE.json](ExampleFiles/ForecastingInitSingleWithCE.json)
3. [ForecastingInitSingleWithoutCE.json](ExampleFiles/ForecastingInitSingleWithoutCE.json)
4. [ForecastingInitDoubleWithCE.json](ExampleFiles/ForecastingInitDoubleWithCE.json)
5. [ForecastingSessionSingleWithCE.json](ExampleFiles/ForecastingSessionSingleWithCE.json)
6. [ForecastingSessionSingleWithoutCE.json](ExampleFiles/ForecastingSessionSingleWithoutCE.json)
7. [ForecastingSessionDoubleWithCE.json](ExampleFiles/ForecastingSessionDoubleWithCE.json)

## Appendix - Steps > 1

We can use `Forecast` to recursively add each `Observation` to the `SlidingWindow` and perform the forecast more than one step ahead. 

Obviously, the predictions will be quite on the pessimistic side.

## Appendix - The algorithm

Let's explain the algorithm on which the library is based by using an example.

This is our trusty `SlidingWindow`:

| <sub>Id</sub> | <sub>X_Actual</sub> | <sub>Y_Forecasted</sub> |
|---|---|---|
| <sub>1</sub> | <sub>58,5</sub> | <sub>615,26</sub> |
| <sub>2</sub> | <sub>615,26</sub> | <sub>659,84</sub> |
| <sub>3</sub> | <sub>659,84</sub> | <sub>635,69</sub> | 
| <sub>4</sub> | <sub>635,69</sub> | <sub>612,27</sub> |
| <sub>5</sub> | <sub>612,27</sub> | <sub>632,94</sub> |
| <sub>6</sub> | <sub>632,94</sub> | <sub>`[NULL]`</sub> |

The first thing we do is to divide each `X_Actual` for the corresponding `Y_Forecasted`:

| <sub>Id</sub> | <sub>XByY</sub>  |
|---|---|
| <sub>1</sub> | <sub>0,1</sub> |
| <sub>2</sub> | <sub>0,93</sub> |
| <sub>3</sub> | <sub>1,04</sub> |
| <sub>4</sub> | <sub>1,04</sub> |
| <sub>5</sub> | <sub>0,97</sub> |

Then, we do calculate C by averaging all the values in the `XByY` column:

| <sub>C</sub> |
|---|
| <sub>0,82</sub> |

At this point, we do substract `C` from each values in `XByY`:

| <sub>Id</sub> | <sub>(XByY)-C</sub>  |
|---|---|
| <sub>1</sub> | <sub>-0,72</sub> |
| <sub>2</sub> | <sub>0,11</sub> |
| <sub>3</sub> | <sub>0,22</sub> |
| <sub>4</sub> | <sub>0,22</sub> |
| <sub>5</sub> | <sub>0,15</sub> |

Calculating the MODE of `(XByY)-C` will return the error `E`:

| <sub>E</sub> |
|---|
| <sub>0,22</sub> |

The function to forecast the next value in the series is `Y=F(X)+E`, which can be expressed as `Y=CX+E`, where X is the actual value. Now that we have both `C` and `E`, it's just a matter of replacing them in the equation to obtain the `Y_Forecasted` value we are looking for:

| <sub>Y_Forecasted</sub> |
|---|
| <sub>519,23</sub> |

The library offers the possibility to skip all the calculations above and provide the C and E coefficients by yourself.

## Markdown Toolset

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)
