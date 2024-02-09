using System;
using System.Collections;
using UnityEngine;

public class chip : MonoBehaviour {
   public KMBombInfo Bomb;
   public KMAudio Audio;
   public KMSelectable chipkms;
   public GameObject thing;

   static int ModuleIdCounter = 1;
   int ModuleId;
   bool ModuleSolved;

   void Awake () {
      ModuleId = ModuleIdCounter++;
      KMSelectable kmselectable = chipkms;
      kmselectable.OnInteract += delegate () { GetComponent<KMBombModule>().HandlePass(); return false; };
   }

   void Start () {
      StartCoroutine(spin());
      StartCoroutine(move());
   }

   IEnumerator spin () {
      while (true) {
         float duration = 0.5f;
         for (float elapsed = 0f; elapsed < duration; elapsed += Time.deltaTime) {
            thing.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(359f, 0f, elapsed / duration));
            yield return null;
         }
      }
   }

   IEnumerator move () {
      while (true) {
         float duration = 0.5f;
         for (float elapsed = 0f; elapsed < duration; elapsed += Time.deltaTime) {
            thing.transform.localPosition = new Vector3(0f, 0.0714f, Mathf.Lerp(0f, -0.01f, elapsed / duration));
            yield return null;
         }
         for (float elapsed = 0f; elapsed < duration; elapsed += Time.deltaTime) {
            thing.transform.localPosition = new Vector3(0f, 0.0714f, Mathf.Lerp(-0.01f, 0f, elapsed / duration));
            yield return null;
         }
      }
   }

#pragma warning restore 414
   private readonly string TwitchHelpMessage = "!{0} chip";
#pragma warning restore 414
   IEnumerator ProcessTwitchCommand (string Command) {
      yield return null;
      if (Command.ToLower() == "chip")
         chipkms.OnInteract();
   }

   IEnumerator TwitchHandleForcedSolve () {
      yield return null;
      chipkms.OnInteract();
   }
}