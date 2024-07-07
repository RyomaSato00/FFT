public class FFT
{
    public static readonly int DataLength = 100;

    public static readonly int[] Order =
    [0, 4, 8, 12, 16, 2, 6, 10, 14, 18, 1, 5, 9, 13, 17, 3, 7, 11, 15, 19];

    public static void Convert(Complex[] input)
    {
        Complex[,] temporary = new Complex[20,5];

        // 並び替え
        for(var i = 0; i < input.Length; i++)
        {
            temporary[i % 20, i / 20] = input[i];
        }

        for(var i = 0; i < 20; i++)
        {
            for(var j= 0; j < 5; j++)
            {
                input[5 * i + j] = temporary[Order[i], j];
            }
        }


        Complex[] part = new Complex[5];
        var unitAngle = 2 * Math.PI / 5;

        for(var partSize = 5; partSize < 25; partSize *= 5)
        {
            var partSizeSplit = partSize / 5;

            for(var offset = 0; offset < DataLength; offset += partSize)
            {
                var angle = 0.0;

                for(var partIndex = 0; partIndex < partSizeSplit; partIndex++)
                {
                    part[0] = input[offset + partIndex];

                    part[1] = input[offset + partIndex + partSizeSplit] * Pole(angle);

                    part[2] = input[offset + partIndex + 2 * partSizeSplit] * Pole(2 * angle);

                    part[3] = input[offset + partIndex + 3 * partSizeSplit] * Pole(3 * angle);

                    part[4] = input[offset + partIndex + 4 * partSizeSplit] * Pole(4 * angle);

                    input[offset + partIndex] = Sum(part, 0);

                    input[offset + partIndex + partSizeSplit] = Sum(part, 2 * Math.PI / 5);

                    input[offset + partIndex + 2 * partSizeSplit] = Sum(part, 2 * Math.PI * 2 / 5);

                    input[offset + partIndex + 3 * partSizeSplit] = Sum(part, 2 * Math.PI * 3 / 5);

                    input[offset + partIndex + 4 * partSizeSplit] = Sum(part, 2 * Math.PI * 4 / 5);

                    angle += unitAngle;
                }

                unitAngle /= 5;
            }
        }
    }

    public static void Inverse(Complex[] input)
    {

    }

    private static Complex Pole(double angle)
    {
        return new Complex
        {
            Real = Math.Cos(angle),
            Imaginary = Math.Sin(angle)
        };
    }

    private static Complex Sum(Complex[] part, double drift)
    {
        return part[0] + part[1] * Pole(drift) + part[2] * Pole(2 * drift) + part[3] * Pole(3 * drift) + part[4] * Pole(4 * drift);
    }
}

public struct Complex
{
    public double Real { get; set; } = 0;
    public double Imaginary { get; set; } = 0;

    public Complex(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public Complex() { }

    public static Complex operator +(Complex a, Complex b)
    {
        return new Complex
        {
            Real = a.Real + b.Real,
            Imaginary = a.Imaginary + b.Imaginary
        };
    }

    public static Complex operator -(Complex a, Complex b)
    {
        return new Complex
        {
            Real = a.Real - b.Real,
            Imaginary = a.Imaginary - b.Imaginary
        };
    }

    public static Complex operator *(Complex a, Complex b)
    {
        return new Complex
        {
            Real = a.Real * b.Real - a.Imaginary * b.Imaginary,
            Imaginary = a.Real * b.Imaginary + a.Imaginary * b.Real
        };
    }

    /// <summary>
    /// 複素共役
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static Complex operator ~(Complex a)
    {
        return new Complex
        {
            Real = a.Real,
            Imaginary = -a.Imaginary
        };
    }
}
