# Open AmbientLED

This started when I had some problems with Gigabyte’s AmbientLED on my [Z390M Gaming](https://www.gigabyte.com/Motherboard/Z390-M-GAMING-rev-10#kf) motherboard. I wasn’t able to use the RGB led without enabling the red led(which doesn’t make sense). Then I decided to check if this was a software problem using the [ILSpy](https://github.com/icsharpcode/ILSpy) to decompile the original software, and it's kind off, you can change the led modes individually, but you can't save this configuration to BIOS since they use only one configuration for both leds.

Then I made this console app to manipulate this configurations, with this you'll be able to write your own scripts and run them at Windows Startup with a tool like the Windows Task Scheduler.

I've been working on a WPF application as well, but still needs an installer and I'm not doing someone now.

## Usage

> You must run this as an Administrator

```bash
$: oambientled.exe [OPTIONS]
```

### Options

First, you must select the led that you want to change:

```text
--rgb
    set the configuration for rgb led

--a, --audio
    set the configuration for audio led
```

> The Audio Led you can only change the mode, and for the rgb led you can change either the mode or color

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
$: oambientled.exe --a --rgb -m still -c blue
```
