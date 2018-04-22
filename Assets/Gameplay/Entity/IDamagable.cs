using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game
{
	public interface IDamagable
	{
        void TakeDamage(float damage, IDamager damager);
	}

    public interface IDamager
    {
        
    }

    public interface ITowerDamager : IDamager
    {
        Tower Tower { get; }
    }
}