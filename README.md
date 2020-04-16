# A program written in C# to demonstrate the Fourier Series
This program was written in C# to demonstrate the calculation of coefficients and hence a periodic function, from any real-valued function.

There are two classes, the Form class, which mainly defines the workings of the form, and the controls therein, and the Fourier Series class, where nearly all the code pertaining to calculation of the Fourier Series, is written.

I also have two classes extending the Exception class, to show the basics of a user-defined exception, i.e. runtime error. These exceptions are designed to be thrown if certain aspects of the input file are not right, for example, any line segment being vertical, or any segments being wholly or partly outside the period boundaries.
