using System;
using System.Collections;
using Dacodelaac.Core;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Dacodelaac.Common
{
    [RequireComponent(typeof(RawImage))]
    public class RandomImage : BaseMono
    {
        const string Url = "https://picsum.photos/seed/{0}/{1}/{2}";
        RectTransform rt;
        RawImage ra;

        public override void Initialize()
        {
            base.Initialize();
            rt = GetComponent<RectTransform>();
            ra = GetComponent<RawImage>();
            ra.color = Color.black;
        }

        public override void DoEnable()
        {
            base.DoEnable();
            StartCoroutine(Fetch());
        }

        IEnumerator Fetch()
        {
            using (var request = UnityWebRequestTexture.GetTexture(string.Format(Url, Time.time, rt.rect.width, rt.rect.height)))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    ra.texture = DownloadHandlerTexture.GetContent(request);
                }
                ra.color = Color.white;
            }
        }
    }
}