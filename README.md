# TI83Sharp

TI-Basic interpreter, compatible with TI-83, TI-83+ and TI-84.

I built this interpreter because I had some old TI-BASIC scripts sitting on my computer that I felt like running again but my TI-83+ is buried in a box somewhere. Figured it was a good excuse to learn a few new things.

> [!NOTE]  
> This interpreter does not replicate the original calculator's execution timing.

## Functionalities

### Variables
Numbers, strings, lists and matrices are supported and have all their native operations implemented.

### Optimizations
I’ve tried to preserve the optimizations available on the original calculator. For example, allowing closing quotes, brackets, or parentheses to be omitted when they appear at the end of a line or just before a store token.

It is missing the ability to chain DelVar commands without separating them with a colon, as well as the ability to omit the starting character of custom list names in certain scenarios. This is why not all tests are successful, as they account for these cases.


### Tokens
| **Category**     | **Token**                                                                 |
|------------------|-------------------------------------------------------------------------------------------|
| **Control Flow** | `If`, `Then`, `Else`, `For(`, `While`, `Repeat`, `End`, `Pause`, `Lbl`, `Goto`, `Menu`, `Return`, `Stop`, `DelVar`, `Ans` |
| **Test**         | `=`, `≠`, `≥`, `<`, `≤`                                                                  |
| **Logic**        | `and`, `or`, `xor`, `not(`                                                               |
| **I/O**          | `Input`, `Disp`, `Output(`, `getKey`, `ClrHome`, `Menu`, `Pause`                                          |
| **Matrices**     | `dim(`, `Fill(`, `augment(`, `cumSum(`                                                   |
| **List**         | `dim(`, `Fill(`, `augment(`, `cumSum(`, `ClrList`, `ClrAllLists`, `SortA(`, `SortD(`, `seq(`, `ΔList(`, `min(`, `max(`, `sum(` |
| **Number**       | `abs(`, `round(`, `iPart(`, `fPart(`, `int(`, `min(`, `max(`                              |
| **Probability**  | `rand`, `randInt(`                                                                       |
| **Math**         | `cos(`, `cos⁻¹(`, `cosh(`, `cosh⁻¹(`, `sin(`, `sinh(`, `sin⁻¹(`, `sinh⁻¹(`, `tan(`, `tan⁻¹(`, `tanh(`, `tanh⁻¹(`, `ln(`, `log(`, `√(`, `³√(` |

## Resources
- [Crafting Interpreters](https://craftinginterpreters.com) by [@munificent](https://github.com/munificent)
- [Let's Build A Simple Interpreter](https://ruslanspivak.com/lsbasi-part1/) by [@rspivak](https://github.com/rspivak/)
    - Used to learn to write an interpreter, very interesting stuff!
- [TI-Basic Developer](http://tibasicdev.wikidot.com/commands)
    - Used to get hardware and software specs
- [TI 84 Calculator Online](https://ti84calc.com/ti84calc)
    - Used to compare my interpreter with a complete emulator
- [Manual Book](https://www.manualslib.com/manual/325936/Texas-Instruments-Ti-83.html#manual)