**Title:** NW.UnivariateForecasting
**Author:** NW (numbworks [AT] gmail [DOT] com)
**Last Update:** 16.08.2020
**Description:**

*Time Series Forecasting* is a *machine learning* technique that aims to predict the next values in a time series when a subset of subsequent timestamped values is provided ("*sliding window*"). There is no other information available than the timestamp and the value itself.

For example, given the last six months of "*Total Monthly Sales USD*" of your company, you would like the machine to predict the amounts for the next x months. 

*Time Series Forecasting* is divided in *Univariate* and *Multivariate*. The first one can predict only one step ahead, while the second one can predict multiple steps ahead. 

As its name states, this library implements the univariate approach. A good definition of "*univariate*" could be"*[...] univariate refers to an expression, equation, function or polynomial of only one variable [...] which consists of observations on only a single characteristic or attribute.*"

Let's imagine to have the following `SlidingWindow` object:

|Id | StartDate | EndDate | TargetDate | Interval | IntervalUnit | Items | ObservationName |
|---|---|---|---|---|---|---|---|
| SW20200803063734 | 2019-01-31 | 2019-07-31 | 2019-08-31 | 6 | Months | 6 | Total Monthly Sales USD |

which contains the following '6' `SlidingWindowItems`:

|Id | StartDate | EndDate | TargetDate | X_Actual | Y_Forecasted |
|---|---|---|---|---|---|
| 1 | 2019-01-31 | 2019-02-28 | 2019-03-31 | 58,5 | 615,26|
| 2 | 2019-02-28 | 2019-03-31 | 2019-04-30 | 615,26 | 659,84 |
| 3 | 2019-03-31 | 2019-04-30 | 2019-05-31 | 659,84 | 635,69 | 
| 4 | 2019-04-30 | 2019-05-31 | 2019-06-30 | 635,69 | 612,27 |
| 5 | 2019-05-31 | 2019-06-30 | 2019-07-31 | 612,27 | 632,94 |
| 6 | 2019-06-30 | 2019-07-31 | 2019-08-31 | 632,94 | `[NULL]` |

Each `SlidingWindowItem` object has the following properties:

- `Id` is a sequential identifier (a number works fine)
- `X_Actual` is the current amount
- `Y_Forecasted` is the next amount in the time series

The `Y_Forecasted` for the last `*Item` is `[NULL]` and it's the value we want to predict.

When fed with the `SlidingWindow` object, the `Forecast()` method of the `UnivariateForecaster` class will output the following `Observation` object

| Name | StartDate | EndDate | Interval | IntervalUnit | X_Actual | C | E | Y_Forecasted | SlidingWindowId |
|---|---|---|---|---|---|---|---|---|---|
| Total Monthly Sales USD | 2019-07-31 | 2019-08-31 | 1 | Months | 632,94 | 0,82 | 0,22 | 519,23 | SW20200803063734 |

The forecasted value we are looking for is: 519,23.


**Example:**

``
Example goes here...
``

**To-do:**

- n/a

**Markdown Toolset:**

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)
