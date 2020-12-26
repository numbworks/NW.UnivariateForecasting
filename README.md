# NW.UnivariateForecasting
Contact: numbworks@gmail.com

## Revision History

| Date | Author | Description |
|---|---|---|
| 2020-12-06 | numbworks | Created. |
| 2020-12-22 | numbworks | Added Revision History. |
| 2020-12-26 | numbworks | Added Download* paragraphs. |

## In Short

**NW.UnivariateForecasting** is a **.NET Standard 2.0** library written in **C#** to perform Univariate Forecasting on your own values. 

From the documentation:

> *Time Series Forecasting* is a *machine learning* technique that aims to predict the next values in a time series when a subset of subsequent timestamped values is provided ("*sliding window*"). There is no other information available than the timestamp and the value itself.

> For example, given the last six months of "*Total Monthly Sales USD*" of your company, you would like the machine to predict the amounts for the next x months.

> *Time Series Forecasting* is divided in *Univariate* and *Multivariate*. 
The first one can predict only one step ahead, while the second one can predict multiple steps ahead.

> As its name states, this library implements the univariate approach. 

## Download the source code

I assume you are on ```Windows```, but the library should compile without issues on Linux as well. Please:

1. Install [Git for Windows](https://git-scm.com/download/win);
2. Open ```Windows Powershell``` (or ```Windows Terminal``` or similar) and type:

```powershell
PS C:\> mkdir NW.UnivariateForecasting
PS C:\> cd .\NW.UnivariateForecasting\
PS C:\NW.UnivariateForecasting> git clone https://github.com/numbworks/NW.UnivariateForecasting.git
```

3. Open ```NW.UnivariateForecasting.sln``` with ```Visual Studio 2015+``` or similar;
4. Done!

## Download the binary packages

If you are a .NET developer and you want to use the library from within your projects, the binary packages are available on [NuGet](https://www.nuget.org/packages/NW.UnivariateForecasting/).

## Getting Started

- [Documentation](docs/Documentation-NW.UnivariateForecaster.md)

## Other Links

- [LICENSE](LICENSE)