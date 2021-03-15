using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace WeappyTest
{
    public class DebugViewer : MonoBehaviour
    {
        private static DebugViewer _instance;

        public static void AddValue(string key, object value)
        {
            _instance._values[key] = value;
        }

        [SerializeField]
        private Text _text;

        private Dictionary<string, object> _values = new Dictionary<string, object>();

        private void Awake()
        {
            _instance = this;
        }

        private void Update()
        {
            if (_values.Count == 0)
                return;
            var maxLength = _values.Keys.Max(k => k.Length);
            _text.text = string.Concat(_values.OrderBy(v => v.Key).Select(v =>
             {
                 var sb = new StringBuilder();
                 sb.Append(v.Key);
                 sb.Append(' ', maxLength - v.Key.Length + 1);
                 sb.Append(v.Value.ToString());
                 sb.AppendLine();
                 return sb.ToString();
             }));
        }
    }
}
