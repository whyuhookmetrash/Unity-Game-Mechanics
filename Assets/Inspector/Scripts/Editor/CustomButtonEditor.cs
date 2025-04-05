using UnityEditor;
using UnityEngine;
using System.Reflection;

[CustomEditor(typeof(MonoBehaviour), true)]
public class CustomButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // ���������� ����������� ���� ����������
        DrawDefaultInspector();

        // �������� ��� ������ � ������� �������
        MethodInfo[] methods = target.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var method in methods)
        {
            // ��������� ������� ������ ��������
            var attributes = method.GetCustomAttributes(typeof(ButtonAttribute), false);
            if (attributes.Length > 0)
            {
                // �������� �������� ������
                string buttonName = ((ButtonAttribute)attributes[0]).ButtonName;

                // ������� ������ � ����������
                if (GUILayout.Button(buttonName))
                {
                    method.Invoke(target, null); // �������� ����� ��� ������� ������
                }
            }
        }
    }
}