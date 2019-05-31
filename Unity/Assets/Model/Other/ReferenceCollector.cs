using System;
using System.Collections.Generic;
using UnityEngine;
//Object并非C#基础中的Object，而是 UnityEngine.Object
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;

#endif

//使其能在Inspector面板显示，并且可以被赋予相应值
[Serializable]
public class ReferenceCollectorData
{
    public string key;
    //Object并非C#基础中的Object，而是 UnityEngine.Object
    public Object gameObject;
}
//继承IComparer对比器，Ordinal会使用序号排序规则比较字符串，因为是byte级别的比较，所以准确性和性能都不错
public class ReferenceCollectorDataComparer : IComparer<ReferenceCollectorData>
{
    public int Compare(ReferenceCollectorData x, ReferenceCollectorData y)
    {
        //在进行调用String.Compare(string1, string2, StringComparison.Ordinal)的时候是进行非语言(non - linguistic)上的比较,
        //API运行时将会对两个字符串进行byte级别的比较,因此这种比较是比较严格和准确的,并且在性能上也很好,
        //一般通过StringComparison.Ordinal来进行比较比使用String.Compare(string1, string2)来比较要快10倍左右.
        //(可以写一个简单的小程序验证, 这个挺让我惊讶, 因为平时使用String.Compare从来就没想过那么多).
        //StringComparison.OrdinalIgnoreCase就是忽略大小写的比较,同样是byte级别的比较.性能稍弱于StringComparison.Ordinal.
        //C# 字符串比较优化（StringComparison）：https://blog.csdn.net/sinat_27657511/article/details/52275327
        return string.Compare(x.key, y.key, StringComparison.Ordinal);
    }
}

//继承ISerializationCallbackReceiver后会增加OnAfterDeserialize和OnBeforeSerialize两个回调函数，如果有需要可以在对需要序列化的东西进行操作
//Dictionary的序列化的泛型解决方案：https://blog.csdn.net/qq_34244317/article/details/79264336
//ET在这里主要是在OnAfterDeserialize回调函数中将data中存储的ReferenceCollectorData转换为dict中的Object，方便之后的使用
//注意UNITY_EDITOR宏定义，在编译以后，部分编辑器相关函数并不存在
public class ReferenceCollector : MonoBehaviour, ISerializationCallbackReceiver
{
    //用于序列化的List        存储的应该就是需要序列化的资源
    public List<ReferenceCollectorData> data = new List<ReferenceCollectorData>();
    //Object并非C#基础中的Object，而是 UnityEngine.Object
    private readonly Dictionary<string, Object> dict = new Dictionary<string, Object>();

#if UNITY_EDITOR
    //添加新的元素
    public void Add(string key, Object obj)
    {
        //unity资源序列化的数据要么是存在meta中，要么就是本身那个资源。
        //比如用Notepad++打开prefab文件后，可能会得到yaml序列化数据。
        //而这些数据都可以通过SerializedProperty获取得到。 https://www.cnblogs.com/CodeGize/p/8697227.html
        //简而言之，前两行代码就是为了找到其中的字段data下面的内容，其内容就是序列化List中存储的数据。
        SerializedObject serializedObject = new SerializedObject(this);
        //根据PropertyPath读取数据：
        //如果不知道具体的格式，可以右键用文本编辑器打开一个prefab文件（如Bundles/UI目录中的几个）
        //因为这几个prefab挂载了ReferenceCollector，所以搜索data就能找到存储的数据
        //相当于拿到了data列表
        //其内容格式为：(每一个被序列化的物体都会有对应的key和gameObject)
        //data:
        //    -key: usernameInputField
        //     gameObject: { fileID: 1027465092727445244}
        SerializedProperty dataProperty = serializedObject.FindProperty("data");
        int i;
        //遍历data，看新添加的数据是否存在相同key
        for (i = 0; i < data.Count; i++)
        {
            if (data[i].key == key)
            {
                break;
            }
        }
        //不等于data.Count意为已经存在于data List中，直接赋值即可
        if (i != data.Count)
        {
            //根据i的值获取dataProperty，也就是data中的对应ReferenceCollectorData，不过在这里，是对Property进行的读取，有点类似json或者xml的节点
            //获取index位置的元素
            SerializedProperty element = dataProperty.GetArrayElementAtIndex(i);
            //对对应节点进行赋值，值为gameobject相对应的fileID
            //fileID独一无二，单对单关系，其他挂载在这个gameobject上的script或组件会保存相对应的fileID
            //将该元素的gameObject属性赋值为obj         (FindPropertyRelative 获取名字相对应的属性）
            element.FindPropertyRelative("gameObject").objectReferenceValue = obj;
        }
        else
        {
            //等于则说明key在data中无对应元素，所以得向其末尾插入新的元素
            dataProperty.InsertArrayElementAtIndex(i);
            SerializedProperty element = dataProperty.GetArrayElementAtIndex(i);
            //获取指定位置的属性后，分别复制key和gameObject
            element.FindPropertyRelative("key").stringValue = key;
            element.FindPropertyRelative("gameObject").objectReferenceValue = obj;
        }
        //应用与更新
        EditorUtility.SetDirty(this);  //通知编辑器更新
        serializedObject.ApplyModifiedProperties(); //应用修改的属性
        serializedObject.UpdateIfRequiredOrScript(); //更新序列化对象的表示，仅当上次调用Update以来对象已被修改或者它是脚本时才更新
    }
    //删除元素，知识点与上面的添加相似
    public void Remove(string key)
    {
        //获取当前物体的preproty属性中的data
        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty dataProperty = serializedObject.FindProperty("data");
        int i;
        //找到key相同的index
        for (i = 0; i < data.Count; i++)
        {
            if (data[i].key == key)
            {
                break;
            }
        }

        if (i != data.Count)
        {
            //这个index存在，就从序列化的属性中删除这个元素
            dataProperty.DeleteArrayElementAtIndex(i);
        }
        //应用与更新
        EditorUtility.SetDirty(this);
        serializedObject.ApplyModifiedProperties();
        serializedObject.UpdateIfRequiredOrScript();
    }

    //清空元素
    public void Clear()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        //根据PropertyPath读取prefab文件中的数据
        //如果不知道具体的格式，可以直接右键用文本编辑器打开，搜索data就能找到
        var dataProperty = serializedObject.FindProperty("data");
        dataProperty.ClearArray();

        EditorUtility.SetDirty(this);
        serializedObject.ApplyModifiedProperties();
        serializedObject.UpdateIfRequiredOrScript();
    }

    public void Sort()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        data.Sort(new ReferenceCollectorDataComparer());  //传入了一个自定义的比较器类，其中重写了比较方法
        EditorUtility.SetDirty(this);
        serializedObject.ApplyModifiedProperties();
        serializedObject.UpdateIfRequiredOrScript();
    }
#endif
    //使用泛型返回对应key的gameobject
    public T Get<T>(string key) where T : class
    {
        Object dictGo;
        if (!dict.TryGetValue(key, out dictGo))
        {
            return null;
        }
        return dictGo as T;
    }

    public Object GetObject(string key)
    {
        Object dictGo;
        if (!dict.TryGetValue(key, out dictGo))
        {
            return null;
        }
        return dictGo;
    }

    public void OnBeforeSerialize()
    {
    }
    //在反序列化后运行
    public void OnAfterDeserialize()
    {
        //清空字典
        dict.Clear();
        //将data列表中的数据全都加入到字典中
        foreach (ReferenceCollectorData referenceCollectorData in data)
        {
            if (!dict.ContainsKey(referenceCollectorData.key))
            {
                dict.Add(referenceCollectorData.key, referenceCollectorData.gameObject);
            }
        }
    }
}
