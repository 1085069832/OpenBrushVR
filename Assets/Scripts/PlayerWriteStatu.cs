using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWriteStatu
{
    private float grade = 100;

    public float Grade
    {
        get
        {
            if (grade < 0)
                grade = 0;

            return grade;
        }

        set
        {
            grade = value;
        }
    }
}
