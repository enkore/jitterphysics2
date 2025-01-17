---
sidebar_position: 1
---

# Project Setup

### Requirements

Install the [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0).

Ensure that dotnet is correctly set up by executing the following command:

```sh
dotnet --version
```

### Create a New Console Application and Add Jitter and Raylib

First, create a new directory named "BoxDrop" and navigate into it:

```sh
mkdir BoxDrop && cd BoxDrop
```

Next, create a new console application in this directory and add Raylib# and Jitter2:

```sh
dotnet new console
dotnet add package Raylib-cs --version 4.5.0.4
dotnet add package Jitter2 --version 2.0.0-alpha
```

You have completed the setup. If you now execute the following command:

```sh
dotnet run
```

Your console should display: "Hello, World!".
