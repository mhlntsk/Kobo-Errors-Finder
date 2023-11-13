# Short structure

1. Entities
   - Row - Represents a row of one particular table. Contains fields corresponding to table columns.
     - `IMyRow` - interface common to rows of all tables.
     - `ConcreteRow1`
     - `ConcreteRow2`
     - ... 
   - Error - implements errors for one particular row. Contains bool fields with all possible error types
     - `IError` - common interface for errors of all tables.
     - `ConcreteError1`
     - `ConcreteError2`
     - ...
2. Application level services
   - `PathReaderService` - Asks the user for the file path. 
3. 3 domain level services
   - `ConductorService` - Strategy for choosing an algorithm for working with the table
   - `HeadersService` - Reads table headers
   - `SheetsService` - Coordinates the work of other sheet-by-sheet services
4. Services for specific processing of tables (specific for each individual table)
   - `Executor` - selected using ConductorService, coordinates the action for a particular table type.
   - `Mapper` - Reads table data.
   - `Operator` - Checks the data for errors.
   - `Printer` - Displays the results of work in the console.


# Detailed description
## Entities

The main project consists of two types of entities that inherit from `IMyRow` and `IError` interfaces.

> Note: The name `IMyRow` is chosen so as not to conflict with `IRow` from the ***NPOI*** library.

For each table that needs a specific check, a separate concrete implementation of the `IMyRow` interface is created, which displays a row from this table and contains all the fields required for checking.

To display the errors that can potentially be found in a row, for each particular implementation of `IMyRow`, corresponding concrete implementations of `IError` are created, which have bool fields with all possible error types.

In the "TablesExtensions" folder, for each individual table type, there are 4 specific services for conducting the process (***Executors***), scanning them (***Mappers***), detecting errors (***Operators***) and displaying the latter on the screen (***Printers***).

More about these specific services a little later.

## Entry point and operation of application-level services
The execution of the program begins with the filling of the DI container (UnityContainer) in the `Main` method. In this particular case, DI-container provides several significant advantages:
1. Helps to reduce the coupling between classes, especially in the case of specific table handlers (Mappers, Operators and Printers) whose specific instances depend on the output of the "strategy" *(see below)*, in this case, hard-coded dependencies would create a lot of extra code.
2. Improving code readability and maintainability.
3. DI-containers help implement the principle of inversion of control, which helps to reduce dependencies on specific implementations. The following point also follows from this.
4. With DI, code becomes more testable and easier to modularize and extend.

After that, an instance of the class `ConductorService` is created, which works according to the strategy principle and is designed to replace the logic, this allows you to dynamically add new types of tables without changing the old code. It is enough to simply create handlers (Executor, Mapper, Operator, Printer) for the new table and register it together with the Executor at the entry point of the program using ConductorService.

:pushpin: **`ConductorService`** contains:
  - Dictionary<string, IExecutor>, the key of which is the name of the table, and the value is the Executor corresponding to this table.
  - The RegisterTable method, which is designed to populate the Dictionary.
  - Absolutely primitive method `GetExecutor(sheet.SheetName)` to get Executors by table name.

Next, a simple loop is launched, which for compactness is enclosed in the StartApplication method of the Program class. It calls a single method of the `PathReaderService` class - `GetPath();`, and if it returns a valid file path, the `CheckAllSheets(filePath);` method of the `SheetsService` class is called.

:pushpin: **`SheetsService`**
`CheckAllSheets(string filePath)`, as you can see, only accepts a file path. In the body, it creates `XSSFWorkbook` from the ***NPOI*** library and does the following in a loop for each page of the document:
1. Receives the page itself in the form of an `ISheet` object.
2. Gets the first row on the header page.
3. Calls `GetHeadersDictionary(firstRowWithHeaders);` of the HeadersService class. The method simply converts the headers from the IRow object (libraries ***NPOI***) into the dictionary `Dictionary<string, int>`, where the key is the name of the column and the value is its serial number.
4. With the help of `ConductorService` gets a specific implementation of `IExecutor` interface. It is on this line that the logic is dynamically substituted with the "Strategy" pattern in the form of `ConcreteExecutor`, which is personal for each type of table.
5. Calls the `Execute(sheet, headersOfSheet);` method on the received specific Executor instance.

## Operation of domain services
:pushpin: **`Executor`**
With the help of DI-container, the executor class receives through the constructor the interfaces of the remaining specific services - Mapper, Operator and Printer (Each executor has its own). In addition to fields for services, Executor also has two fields with auto-properties: `List<IMyRow>` and `List<IError>`, and the `Execute(sheet, headersOfSheet);` method. Since the Executor is a simple conductor that controls the processing logic of a specific table, it calls the methods of each of the processing services in turn, the details of which we will analyze below:
```C#
mapedRowsFromTable = _mapper.Map(sheet, headersOfSheet);

errorsInRows = _operator.Check(mapedRowsFromTable);

_printer.Print(errorsInRows, mapedRowsFromTable);
```
:pushpin: **`Mapper`**
All concrete mappers are inherited from:
1. An interface closed by a specific type `IMapper<T> where T : class` - this allows you to work with public contracts and not with a specific implementation, and at the same time helps the DI-container to understand what type should be assigned to a particular class (which it does not abstract class (see next point)). The public interface contract consists of one method:
    - `Map` - this method is implemented in an **abstract class** and has common logic for all mappers. For specific logic, it calls the **abstract method** `MakeSpecificMapping`, which is **defined** in the abstract class and **implemented** in the concrete one.
2. Abstract class `AbstractMapper`, which has common logic for all mappers in the method `Map` and has an abstract method `MakeSpecificMapping`.

The `MakeSpecificMapping` abstract method has an implementation in each specific class. It calls private mapping methods that are implemented separately in each specific class. Example:

```C#
public class BasicMapper : AbstractAgeValueMapper, IMapper<BasicMapper>
{
    public override IMyRow MakeSpecificMapping(Dictionary<string, int> headersOfSheet, IRow rowFromTable)
    {
        BasicRow myRow = new BasicRow();

        MapMsfPatientId(headersOfSheet, myRow, rowFromTable);

        // інші методи мапінгу, у т.ч. з абстрактного класу

        return myRow;
    }

    public void MapMsfPatientId(Dictionary<string, int> headersOfSheet, BasicRow myRow, IRow rowFromTable)
    {
        // конкретна реалізація
    }

    // реалізація інших методів мапінгу

}
```

:pushpin: **`Operator`**
All concrete operators inherit from `IOperator<T> where T : class`, and work in a similar way to mappers. Each operator has its own private checking classes that are called in the `Check` method implemented by `IOperator`. The interface also has the property `List<IError> errors` Example:
``` C#
public class BasicOperator : IOperator<BasicOperator>
{
    List<IError> errors = new List<IError>();
    public List<IError> Check(List<IMyRow> rows)
    {
        CheckSex(rows, errors);
        // виклик інших Check-методів

        return errors;
    }
    
    public void CheckSex(List<IMyRow> rows, List<IError> errors)
    {
        // Конкретна реалізація
    }

    // Інші Check-методи
}
```

:pushpin: **`Printer`**
All specific printers work according to an identical scheme - `IPrinter<T> where T : class` and `AbstractPrinter` with shared methods `Print` and abstract `MakeSpecificPrinting`, which has an implementation in specific printers and internally calls private methods that are also implemented in specific printers and for each printer are special.