# Open AmbientLED

This started when I had some problems with the Gigabyte’s AmbientLED on my [Z390M Gaming](https://www.gigabyte.com/Motherboard/Z390-M-GAMING-rev-10#kf) motherboard. I wasn’t able to enable the RGB led without enabling the red led, which doesn’t make any sense. Then I decided to check if this was a software problem using the [ILSpy](https://github.com/icsharpcode/ILSpy) to decompile the original software, and to my surprise, I was right! It is a software problem. So, I’ve studied the code and did this one to solve this problem.

## Usage

> You must run this as an Administrator

```bash
$: oambientled.exe [OPTIONS]
```

### Options

First, you must select the led that you want to change:

```text
--ml, --mled, --monocled
    set the configuration for monocled

--al, --aled, --audioled
    set the configuration for audioled
```

> The Audio Led you can only change the mode, and for the other led(MonocLed) you can change either the mode or color

Then you write the options:

```text
-m, --mode=VALUE
    off, darkoff, still, breath, auto, flash, random,
    wave, scene, condition, dflash, colorcycle
```

```text
-c, --color=VALUE
    color name/hex
```

__Example__:

```bash
$: oambientled.exe --al --ml -m still -c blue
```
