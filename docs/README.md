# Kobo Errors Finder

![Application logo](../src/KoboErrorsFinder/bin/Debug/net7.0/KoboErrorsFinderLogo.png "Logo")

![NPOI](https://img.shields.io/nuget/v/NPOI?label=NPOI)
![Unity.Container](https://img.shields.io/nuget/v/Unity.Container?label=Unity.Container)
![NUnit](https://img.shields.io/nuget/v/NUnit?label=NUnit)
![Bogus](https://img.shields.io/nuget/v/Bogus?label=Bogus)
![FluentAssertions](https://img.shields.io/nuget/v/FluentAssertions?label=FluentAssertions)
![NSubstitute](https://img.shields.io/nuget/v/NSubstitute?label=NSubstitute)

## Content

1. [Introduction](#introduction)
2. [Use](#use)
3. [Documentation](#documentation)
4. [Examples](#examples)
5. [License](#license)

## Introduction
The Kobo Errors Finder project is designed to read data from .xlsx tables and efficiently detect and handle internal data errors. The application allows you to automate the process of checking and outputting results, facilitating the analysis of data from various sources.

## Use
This console app doesn't need to be installed. To use it, it is enough to download the project and run its bilded version.

## Documentation
[Documentation](docs/doc.md)

## Examples
If you want to extend the application or customize it for your needs, you need to follow a simple sequence of actions. Let's speed up the script for adding a new table that will be checked (let's imagine calling it "NewSpecificTable"):

1. You need to create a separate entity class - `NewSpecificTableRow`; also create `NewSpecificTableError` to record errors from each row.
2. The entities are ready, you need to take care of the classes that will be their handlers. So, for our `NewSpecificTable`, we also need to create 4 specific handlers: 
   - *NewSpecificTableExecutor* ` : IExecutor`
   - *NewSpecificTableMapper* ` : AbstractMapper, IMapper<NewSpecificTableMapper>`
   - *NewSpecificTableOperator* `: AbstractOperator, IOperator<NewSpecificTableOperator>`
   - *NewSpecificTablePrinter* `: AbstractPrinter, IPrinter<NewSpecificTablePrinter>`
    > 💡 Below we will consider each of them in more detail.
3. After creating the handlers, you need to register them in the DI-container. WE can do this in the Program class:
```C#
IUnityContainer container = new UnityContainer();
// Registration of other services
container.RegisterType<IExecutor<NewSpecificTableExecutor>, NewSpecificTableExecutor>();
container.RegisterType<IMapper<NewSpecificTableMapper>, NewSpecificTableMapper>();
container.RegisterType<IOperator<NewSpecificTableOperator>, NewSpecificTableOperator>();
container.RegisterType<IPrinter<NewSpecificTablePrinter>, NewSpecificTablePrinter>();
```
4. After the handlers are added to DI-container, you need to register a name of sheet of you new table in the "strategy", which will place the Executor to handle your table. We do this in the Programs class:
```C#
var conductorService = container.Resolve<IConductorService>();
conductorService.RegisterTable("NameOfSheet_1", container.Resolve<SpecificExecutor1>());
conductorService.RegisterTable("NewSpecificTableSheet", container.Resolve<NewSpecificTableExecutor>()); // !!
```
Congratulations, your table has been added! (I remind you that we consider examples of creating handlers below!)


### SpecificTableExecutor
When you creat this class, you need to extract 3 handlers from DI. Then create empty lists of rows, which will be filled row-by-row from the table and list of errors, which also corresponds to the rows.

This class contains only one `Execute` method, which does not return anything and accepts as input the current page sheet (ISheet sheet) and a dictionary with headers (Dictionary<string, int> headersOfSheet). If you wish, you can not change the content of the class at all.
```C#
public class BasicExecutor : IExecutor
{
    protected readonly IMapper<BasicMapper> _mapper;
    protected readonly IOperator<BasicOperator> _operator;
    protected readonly IPrinter<BasicPrinter> _printer;
    public List<IMyRow> mapedRowsFromTable { get; set; }
    public List<IError> errorsInRows { get; set; }
    
    public BasicExecutor(
        IMapper<BasicMapper> basicMapper, 
        IOperator<BasicOperator> basicOperator, 
        IPrinter<BasicPrinter> basicPrinter)
    {
        _mapper = basicMapper;
        _operator = basicOperator;
        _printer = basicPrinter;
    }
    public void Execute(ISheet sheet, Dictionary<string, int> headersOfSheet)
    {
        mapedRowsFromTable = _mapper.Map(sheet, headersOfSheet);

        errorsInRows = _operator.Check(mapedRowsFromTable);

        _printer.Print(errorsInRows, mapedRowsFromTable);
    }
}
```

### SpecificTableMapper
This class implements the `MakeSpecificMapping` abstract method from `AbstractMapper` and has its own private Map methods for your specific table headers:
```C#
public class BasicMapper : AbstractMapper, IMapper<BasicMapper>
{
    public override IMyRow MakeSpecificMapping(Dictionary<string, int> headersOfSheet, IRow rowFromTable)
    {
        BasicRow myRow = new BasicRow();

        MapSpecific1(headersOfSheet, myRow, rowFromTable);

        MapSpecific2(headersOfSheet, myRow, rowFromTable);

        // Other map-methods

        return myRow;
    }

    private void MapSpecific1(Dictionary<string, int> headersOfSheet, BasicRow myRow, IRow rowFromTable)
    {
        if (headersOfSheet.ContainsKey("NameOfColumn_1"))
        {
            int columnIndex = headersOfSheet["NameOfColumn_1"];
            myRow.SpecificFildFromSpecificTableRow_1 = rowFromTable.GetCell(columnIndex)?.ToString(); // Field from your Row-entity
        }
    }
    private void MapSpecific2(Dictionary<string, int> headersOfSheet, BasicRow myRow, IRow rowFromTable)
    {
        if (headersOfSheet.ContainsKey("NameOfColumn_2"))
        {
            int columnIndex = headersOfSheet["NameOfColumn_2"];
            myRow.Sex = SpecificFildFromSpecificTableRow_2.GetCell(columnIndex)?.ToString(); // Field from your Row-entity
        }
    }

}
```

### SpecificTableOperator
In this class, you need to create your checker-methods and call them one by one in the implemented Check method. You need to sort the rows as necessary in the specific-methods and create your own condition, for example: `if (rowsWithoutSpecifyLocation != null && rowsWithoutSpecifyLocation.Count() > 0)`. The rest of the logic changes as needed.

You also need to **override** the abstract class `GetError();` so that the abstract class in its method can get the error entity that matches exactly your RowEntity.

```C#
public class SpecificTableOperator : AbstractOperator, IOperator<SpecificTableOperator>
{
    public List<IError> errors { get; set; } = new List<IError>();
    public List<IError> Check(List<IMyRow> rows)
    {
        CheckSpecificPossibleError_1(rows, errors);
        CheckSpecificPossibleError_2(rows, errors);
        // You can add more Check-methods

        return errors;
    }

    public void CheckSpecificPossibleError_1(List<IMyRow> rows, List<IError> errors)
    {
        List<SpecificTableRow> ambulanceRows = rows.Cast<SpecificTableRow>().ToList();

        var rowsWithoutSpecifyLocation = ambulanceRows // It`s just example
            .Where(p =>
                p.RegionOfPatient == "Other location" &&
                p.TypeOfRequester == "Health facility" &&
                p.OtherPatientLocationSpecify == null);

        if (rowsWithoutSpecifyLocation != null && rowsWithoutSpecifyLocation.Count() > 0)
        {
            foreach (var row in rowsWithoutSpecifyLocation)
            {
                SpecificTableError error = null;

                if (errors != null && errors.Count != 0)
                {
                    error = (SpecificTableError)errors.FirstOrDefault(e => e.UniqueEntityId == row.UniqueEntityId);
                }

                if (error == null)
                {
                    error = new SpecificTableError()
                    {
                        UniqueEntityId = row.UniqueEntityId,
                        OtherPatientLocationSpecifyIsEmpty = true
                    };
                }
                else
                {
                    error.OtherPatientLocationSpecifyIsEmpty = true;
                }

                errors.Add(error);
            }
        }

    }
    public override IAgeError GetError()
    {
        return new SpecificTableError();
    }

}
```

### SpecificTablePrinter
Так як цей клас прямо не впливає на основну логіку, ви можете змінювати його як вам заманеться. Основна структура така - AbstractMapper перевіряє наявність помилок в поточній таблиці, якщо їх немає, він виводить про це повідомлення, якщо помилки знайдені - запускає `SpecificTablePrinter.MakeSpecificPrinting();`.
```C#
public class SpecificTablePrinter : AbstractPrinter, IPrinter<SpecificTablePrinter>
{
    public override void MakeSpecificPrinting(List<IError> errors, List<IMyRow> rows)
    {
        var castedErrors = errors.Cast<BasicError>().ToList();
        var castedRows = rows.Cast<BasicRow>().ToList();

        var joinedErrorsAndRows = from error in castedErrors
                                  join row in castedRows on error.UniqueEntityId equals row.UniqueEntityId
                                  select new { Error = error, Row = row };

        var groupedByIdErrors = joinedErrorsAndRows
            .OrderBy(item => item.Row.MSFPatientID)
            .GroupBy(item => item.Row.MSFPatientID)
            .ToList();

        int counter = 1;

        foreach (var groupOfErrors in groupedByIdErrors)
        {
            var errorsById = groupOfErrors.ToList();

            Console.WriteLine($"\n{counter}) {errorsById.First().Row.MSFPatientID}");

            if (errorsById.Any(error => error.Error.IdError == true))
            {
                Console.WriteLine("\tError in field \"MSF Patient Id\"");
            }

            // Other specific printing
            
            Console.WriteLine($"\t\t {errorsById.First().Row.AgeValue} | {errorsById.First().Row.Sex} | {errorsById.First().Row.Date}");

            counter++;
        }
    }
}
```

## Libraries Used
- [NPOI](https://github.com/nissl-lab/npoi) - Apache NPOI
- [Unity.Container](https://github.com/unitycontainer/container) - Unity Container
- [NUnit](https://nunit.org/) - NUnit
- [Bogus](https://github.com/bchavez/Bogus) - Bogus
- [FluentAssertions](https://fluentassertions.com/) - Fluent Assertions
- [NSubstitute](https://nsubstitute.github.io/) - NSubstitute

## License
[License](docs/LICENSE.txt)