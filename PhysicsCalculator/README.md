# ⚛ Physics Scientific Calculator

**Author:** Vio Gustian Nur Alamsyah  
**Year:** 2025  
**Runtime:** .NET 10 · Windows Forms  

---

## Requirements

| Tool | Version |
|------|---------|
| Visual Studio | 2022 (v17.x) or later |
| .NET SDK | **10.0** |
| OS | Windows 10 / 11 |

> **.NET 10 SDK download:** https://dotnet.microsoft.com/download/dotnet/10.0

---

## How to Open & Build

1. Extract the zip — you get:
   ```
   PhysicsCalculator.sln
   PhysicsCalculator/
     PhysicsCalculator.csproj
     Program.cs
     Form1.cs
     Form1.Designer.cs
     FormAbout.cs
     FormConstant.cs
     PhysicsConstant.cs
   ```

2. Double-click **`PhysicsCalculator.sln`** — Visual Studio opens automatically.

3. Press **`Ctrl + Shift + B`** to build, or **`F5`** to build and run.

That's it — no NuGet packages, no extra setup.

---

## Or build from terminal

```
dotnet build
dotnet run
```

---

## Features

- Scientific functions: sin, cos, tan, asin, acos, atan, log, ln, sqrt, cbrt, exp, abs, n!
- Power operators: x², x³, xⁿ, 1/x
- Constants: π, e
- DEG / RAD toggle
- 20 built-in physics constants (CODATA 2022)
- Add / Edit / Delete custom constants
- Calculation history (last 10)
- Full keyboard support
- Help → About dialog
