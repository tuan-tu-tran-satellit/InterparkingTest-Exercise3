This solution contains the solution to the exercise 3 of the Interparking test: write and evolve a file reading library.
The exercise description can be found [here](https://satellitbe-my.sharepoint.com/:b:/g/personal/tuan-tu_tran_satellit_be/ESrO8LcH4s1Ai1x7530VeT0BOorhEVdDQaPRRxFra_KSJw?e=8Erfyc)

The library lives in the `InterParkingTestFileReadinLib` project where there are multiple classes to address the different use cases.
They're built by combining multiple base classes which implement different fonctionatilites, using the *decorator pattern*.

The classes of the library are tested in the `Tests` project, which uses Moq to mock file system access and FluentAssertions to assert
test results.

Finally there's a console application that implements the bonus part of the exercise. It reads the user preferences and builds a file
reader object based on those preferences and allows the user to exercise that reader on multiple sample files.