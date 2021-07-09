using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
namespace test02
{
    [InitializeOnLoad]
    public class TestEditor
    {
         static TestEditor()
        {
            Debug.Log("编辑器运行");
        }
    }
}
