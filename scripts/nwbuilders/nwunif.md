% nwunif

# NAME
nwunif - perform univariate forecasting tasks on your values

# SYNOPSIS
**nwunif** [command] [options]

# DESCRIPTION
**nwunif** is a CLI application to perform univariate forecasting tasks on the values you provide. 

# OPTIONS
**-?, -h, --help**
Show help and usage information.

# COMMANDS

### session forecast
Performs a univariate forecasting task on the values you provide. 

**--init:{filename}**
    Defines the name of the JSON file containing the known values. This file must be located in the working folder.

**--folderpath:{path}**
    Defines the custom path to the working directory where the application will look for input files and save any session outputs, overriding the default application folder.

**--roundingdigits:{number}**
    Defines the decimal precision used when rounding auto‑generated coefficient and error values. The number of digits cannot exceed DefaultRoundingDigits (15).

**--forecastingdenominator:{number}**
    Defines the fallback divisor used to prevent divide‑by‑zero when a forecasted value in a SlidingWindowItem is exactly zero. The default is a very small positive number to ensure safe normalization. Default: 0.00000000000001.

**--savesession**
    Defines whether the application should automatically save the results, metrics, and logs of the current forecasting session to the working folder upon completion.

**--enableutc**
    Replaces local time with UTC time in timestamps.

# EXAMPLES
**Performs a univariate forecasting task on the values you provide:**

```text
./nwunif session forecast --init:Init.json
```

# AUTHOR
numbworks (numbworks@gmail.com)