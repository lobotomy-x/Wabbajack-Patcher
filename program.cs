using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.IO;
using Microsoft.Win32;
using System.Windows;


string wabbapath = "";
if ( args.Length > 0 )
    if ( args [ 1 ].Contains ( "Wabbajack.dll" ) )
        wabbapath = args [ 1 ];
var wabba_dll = Path.Combine ( wabbapath.Length > 0 ? wabbapath : Directory.GetCurrentDirectory ( ), "wabbajack.dll" );

int totalNumTypes = 0;
var wip_wabba = Path.Combine(Directory.GetCurrentDirectory ( ), "wabbajackold.dll");
if (!File.Exists(wip_wabba))
    File.Move ( wabba_dll, wip_wabba );

ModuleContext modCtx = ModuleDef.CreateModuleContext ( );
ModuleDefMD module = ModuleDefMD.Load ( wip_wabba, modCtx );
foreach ( var type in module.GetTypes ( ) )
    {
    if (type.Namespace == "Wabbajack" )
        {
        if ( type.Name.Contains("App" ))
            {
            Console.WriteLine ( type );

            foreach ( var method in type.Methods )
                {

                if ( method.Name.Contains ( "IsAdmin" ) )
                    {                
                    Console.WriteLine ( method.Name );
                    Console.Write ( method.Body.ToString ( ) );
                    CilBody body;
                    method.Body = body = new CilBody ( );
                    // Add instructions to load the constant 'false' onto the evaluation stack
                    body.Instructions.Add ( OpCodes.Ldc_I4_0.ToInstruction ( ) );

                    // Add the return instruction
                    body.Instructions.Add ( OpCodes.Ret.ToInstruction ( ) );
                    Console.Write ( body.ToString ( ) );
                    }
                }
            }

        }


   
    }
module.Write ( wabba_dll );
