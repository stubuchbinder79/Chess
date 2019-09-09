using JetBrains.Annotations;
using UnityEngine;

public interface ISelector
{ 
        void EnterState([CanBeNull] GameObject go);
        void ExitState([CanBeNull] GameObject go);
}
