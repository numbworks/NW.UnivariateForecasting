# NW.UnivariateForecasting

**Author:** NW
**Email:** numbworks [AT] gmail [DOT] com
**Last Update:** 28.11.2020

### Introduction

*Time Series Forecasting* is a *machine learning* technique that aims to predict the next values in a time series when a subset of subsequent timestamped values is provided ("*sliding window*"). There is no other information available than the timestamp and the value itself.

For example, given the last six months of "*Total Monthly Sales USD*" of your company, you would like the machine to predict the amounts for the next x months. 

*Time Series Forecasting* is divided in *Univariate* and *Multivariate*. The first one can predict only one step ahead, while the second one can predict multiple steps ahead. 

As its name states, this library implements the univariate approach. A good definition of "*univariate*" could be"*[...] univariate refers to an expression, equation, function or polynomial of only one variable [...] which consists of observations on only a single characteristic or attribute.*"

### Getting Started 

In order to use the library, the first thing we need to initialize a `UnivariateForecastingSettings` object, which collects all the required settings in one place:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using NW.UnivariateForecasting;

/* ... */

UnivariateForecastingSettings settings = new UnivariateForecastingSettings();

/* ... */
```

The `UnivariateForecastingSettings` object looks like the following:

```csharp
public UnivariateForecastingSettings(
        Func<string> idCreationFunction = null,
        Func<double, double> roundingFunction = null,
        Action<string> loggingAction = null,
        double forecastingDenominator = 0.001,
        string dummyId = null,
        string dummyObservationName = null,
        DateTime dummyStartDate = default(DateTime),
        uint dummySteps = default(uint),
        IntervalUnits dummyIntervalUnit = default(IntervalUnits)
    ) { /* ... */ }
```

If the `UnivariateForecastingSettings` parameters won't be set, a default value will be assigned to each of them.

The next step is to create a `SlidingWindow` object to host the time series, and the library provides a  `SlidingWindowManager` helper class that aids the process: 

```csharp
/* ... */
string slidingWindowId = settings.IdCreationFunction.Invoke(); // SW{yyyyMMddhhmmsss}
string observationName = "Total Monthly Sales USD";
List<double> values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();
uint steps = 1;
IntervalUnits intervalUnit = IntervalUnits.Months;
DateTime startDate = new DateTime(2019, 01, 31, 00, 00, 00);

ISlidingWindowManager slidingWindowManager = new SlidingWindowManager(settings);
SlidingWindow slidingWindow 
    = slidingWindowManager.Create(slidingWindowId, observationName, values, steps, intervalUnit, startDate);

/* ... */
```

The `SlidingWindow` object will look like this:

| <sub>Id</sub> | <sub>ObservationName</sub> | <sub>Interval</sub> | <sub>Items</sub> | 
|---|---|---|---|
| <sub>SW20200803063734</sub> | <sub>Total Monthly Sales USD</sub> | <sub>6:Months:20190131:20190731:20190831:1:6</sub> | <sub>6</sub> |

which contains the following '6' `SlidingWindowItems`:

| <sub>Id</sub> | <sub>Interval</sub> | <sub>X_Actual</sub> | <sub>Y_Forecasted</sub> |
|---|---|---|---|
| <sub>1</sub> | <sub>20190131:20190228:20190331</sub> | <sub>58,5</sub> | <sub>615,26</sub> |
| <sub>2</sub> | <sub>20190228:20190331:20190430</sub> | <sub>615,26</sub> | <sub>659,84</sub> |
| <sub>3</sub> | <sub>20190331:20190430:20190531</sub> | <sub>659,84</sub> | <sub>635,69</sub> | 
| <sub>4</sub> | <sub>20190430:20190531:20190630</sub> | <sub>635,69</sub> | <sub>612,27</sub> |
| <sub>5</sub> | <sub>20190531:20190630:20190731</sub> | <sub>612,27</sub> | <sub>632,94</sub> |
| <sub>6</sub> | <sub>20190630:20190731:20190831</sub> | <sub>632,94</sub> | <sub>`[NULL]`</sub> |

Each `SlidingWindowItem` object has the following properties:

- `Id` is a sequential identifier (a number works fine in this case)
- `X_Actual` is the current amount
- `Y_Forecasted` is the next amount in the time series

The `Y_Forecasted` for the last `*Item` is `[NULL]` and it's the value we want to predict.

Once the `SlidingWindow` object is set, we are ready to perform the prediction itself by using the `UnivariateForecaster` class:

```csharp
/* ... */
IUnivariateForecaster forecaster = new UnivariateForecaster(settings);
Observation observation = forecaster.Forecast(slidingWindow);
```

This will return an `Observation` object, which will look like the following:

| <sub>Name</sub> | <sub>Interval</sub> | <sub>X_Actual</sub> | <sub>C</sub> | <sub>E</sub> | <sub>Y_Forecasted</sub> | <sub>SlidingWindowId</sub> |
|---|---|---|---|---|---|---|---|---|---|
| <sub>Total Monthly Sales USD</sub> | <sub>1:Months:20190731:20190831:20190930:1:1</sub> | <sub>632,94</sub> | <sub>0,82</sub> | <sub>0,22</sub> | <sub>519,23</sub> | <sub>SW20200803063734</sub> |

The original time series was: `{ 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }`.
According to the univariate forecasting, the next value of the series will be: `519,23`.

### Less is More?

If we do have only a list of values without any specific time stamps, one of the `SlidingWindowManager.Create()` overloads can create a dummy `SlidingWindow` around them for us:

```csharp
/* ... */
UnivariateForecastingSettings settings = new UnivariateForecastingSettings();
List<double> values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();

ISlidingWindowManager slidingWindowManager = new SlidingWindowManager(settings);
SlidingWindow slidingWindow = slidingWindowManager.Create(values);
IUnivariateForecaster forecaster = new UnivariateForecaster(settings);
Observation observation = forecaster.Forecast(slidingWindow);
```
The dummy `SlidingWindow` will look like this:

| <sub>Id</sub> | <sub>ObservationName</sub> | <sub>Interval</sub> | <sub>Items</sub> | 
|---|---|---|---|
| <sub>Dummy Id</sub> | <sub>Dummy Observation</sub> | <sub>6:Months:20200101:20200701:20200801:1:6</sub> | <sub>6</sub> |

The dummy values can be customized in `UnivariateForecastingSettings`.

### One, Two, Three, ...

If we can predict values for more than one step ahead, we can use `ForecastAndCombine` to recursively add each observation to the `SlidingWindow` and perform the forecasting on it:

```csharp
/* ... */
UnivariateForecastingSettings settings = new UnivariateForecastingSettings();
List<double> values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();

ISlidingWindowManager slidingWindowManager = new SlidingWindowManager(settings);
SlidingWindow slidingWindow = slidingWindowManager.Create(values);
IUnivariateForecaster forecaster = new UnivariateForecaster(settings);
Observation observation = forecaster.Forecast(slidingWindow);

SlidingWindow newSlidingWindow = forecaster.ForecastAndCombine(slidingWindow, 3);
List<double> results = forecaster.ExtractXActualValues(newSlidingWindow);
```

The resulting `SlidingWindow` will contain the following '9' `SlidingWindowItems`:

| <sub>Id</sub> | <sub>Interval</sub> | <sub>X_Actual</sub> | <sub>Y_Forecasted</sub> |
|---|---|---|---|
| <sub>1</sub> | <sub>20200101:20200201:20200301</sub> | <sub>58,5</sub> | <sub>615,26</sub> |
| <sub>2</sub> | <sub>20200201:20200301:20200401</sub> | <sub>615,26</sub> | <sub>659,84</sub> |
| <sub>3</sub> | <sub>20200301:20200401:20200501</sub> | <sub>659,84</sub> | <sub>635,69</sub> | 
| <sub>4</sub> | <sub>20200401:20200501:20200601</sub> | <sub>635,69</sub> | <sub>612,27</sub> |
| <sub>5</sub> | <sub>20200501:20200601:20200701</sub> | <sub>612,27</sub> | <sub>632,94</sub> |
| <sub>6</sub> | <sub>20200601:20200701:20200801</sub> | <sub>632,94</sub> | <sub>519,23</sub> |
| <sub>7</sub> | <sub>20200701:20200801:20200901</sub> | <sub>519,23</sub> | <sub>457,08</sub> |
| <sub>8</sub> | <sub>20200801:20200901:20201001</sub> | <sub>457,08</sub> | <sub>420,63</sub> |
| <sub>9</sub> | <sub>20200901:20201001:20201101</sub> | <sub>420,63</sub> | <sub>`[NULL]`</sub> |

Obviously, the predictions will be quite on the pessimistic side.

The `ExtractXActualValues` method helps extracting the provided and the forecasted values in the same list for convenience, which in this case will look like this: `[58,5, 615,26, 659,84, 635,69, 612,27, 632,94, 519,23, 457,08, 420,63]`.

### In a Hurry

If you are really in a hurry, you can predict the next value in a time series with only four lines of code:

```csharp
UnivariateForecastingSettings settings = new UnivariateForecastingSettings();
List<double> values = new[] { 58.50, 615.26, 659.84, 635.69, 612.27, 632.94 }.ToList();
IUnivariateForecaster forecaster = new UnivariateForecaster(settings);
double nextValue = forecaster.ForecastNextValue(values);
```

This scenario hasn't been shows as the first one, because it was important to explain the forecasting together with the concept of `SlidingWindow` first.

### Markdown Toolset

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)
