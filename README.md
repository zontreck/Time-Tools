TIME TOOLS
==========

This software is a very simple command line utility that I wrote when I needed something with these capabilities.

This program can be hooked into a script easily, which is my use case.

Here is the current program's usage:

```
Time Tools 1.0.0.5
Copyright (C) 2022 ZNI Creations

  -a, --add             (Default: false) Add together two ZNI Timestamps

  -s, --sub             (Default: false) Subtract ZNI Timestamps

  -u, --unix            (Default: false) Outputs unix time instead of a ZNI Timestamp

  -t, --times           The time list

  -g, --get             (Default: false) Similar to current unix, this will make time operations relative to the current
                        time in the output. This can result in large timestamps

  -c, --current-unix    If unix time is set, will return the unix time with difference applied instead of only the
                        difference

  -o, --from-unix       Converts provided integer into a time string

  --help                Display this help screen.

  --version             Display version information.

Please specify command line arguments to use this program. Use --help for the help page
```


PLANNED FEATURES
==========

As this is a hobby program, open sourced mainly to be put on a resume to show code examples, there's not many planned features except perhaps a console GUI just for fun.


COMPILING
===========

This program is written in C#, and uses Dot Net 6. You can compile the program with the following command

```
dotnet build
```

It really is that simple, provided you have the dotnet sdk installed.