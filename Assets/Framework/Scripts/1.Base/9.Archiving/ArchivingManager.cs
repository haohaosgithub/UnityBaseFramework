using Sirenix.OdinInspector.Editor.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Framework
{
    public class ArchivingItem
    {
        public int archivingId;

        private DateTime lastUpdateTime;
        public DateTime LastUpdateTime
        {
            get
            {
                return lastUpdateTime;
            }
            set
            {
                lastUpdateTime = value;
                ArchivingManager.Instance.SaveArchivingMetaData();
            }
        }
        public ArchivingItem(int id, DateTime time)
        {
            archivingId = id;
            LastUpdateTime = time;
        }
    }
    public class ArchivingMetaData
    {
        public int curID;
        public List<ArchivingItem> archivingItems = new List<ArchivingItem>();
    }
    public class ArchivingManager : ManagerBase<ArchivingManager>
    {
        BinaryFormatter bf = new BinaryFormatter();
        string archivingPath;
        Dictionary<int, Dictionary<string, object>> saveCache; //模拟存档结构的缓存(存档ID,(文件名，对象))
       
        
        ArchivingMetaData archivingMetaData;
        public override void Init()
        {
            base.Init();
            archivingPath = Application.persistentDataPath + "/" + "SaveData";
            archivingMetaData = new ArchivingMetaData();
            if(!Directory.Exists(archivingPath))
            {
                Directory.CreateDirectory(archivingPath);
            }
            saveCache = new Dictionary<int, Dictionary<string, object>>();
        }
        #region 存档元数据
        /// <summary>
        /// 创建一个新存档
        /// </summary>
        public ArchivingItem CreateArchivingItem()
        {
            ArchivingItem item = new ArchivingItem(archivingMetaData.curID,DateTime.Now);
            archivingMetaData.archivingItems.Add(item);
            ++archivingMetaData.curID;
            SaveArchivingMetaData();
            return item;
        }
        /// <summary>
        /// 删除一个存档
        /// </summary>
        /// <param name="archivingID">存档ID</param>
        public void DeleteArchivingItem(int archivingID)
        {
            string path = GetOneArchivingPath(archivingID,false);
            if(path != null)
            {
                Directory.Delete(path, true);
            }
            archivingMetaData.archivingItems.Remove(GetArchivingItem(archivingID));
        }
        /// <summary>
        /// 删除一个存档
        /// </summary>
        /// <param name="archivingItem">存档对象</param>
        public void DeleteArchivingItem(ArchivingItem archivingItem)
        {
            string path = GetOneArchivingPath(archivingItem.archivingId,false);
            if (path != null)
            {
                Directory.Delete(path, true);
            }
            archivingMetaData.archivingItems.Remove(archivingItem);
        }
        /// <summary>
        /// 获取存档对象
        /// </summary>
        /// <param name="archivingID">存档ID</param>
        /// <returns></returns>
        public ArchivingItem GetArchivingItem(int archivingID)
        {
            for(int i = 0;i< archivingMetaData.archivingItems.Count;i++)
            {
                if(archivingMetaData.archivingItems[i].archivingId == archivingID)
                {
                    return archivingMetaData.archivingItems[i];
                }
            }
            return null;
        }
        /// <summary>
        /// 保存存档元数据
        /// </summary>
        public void SaveArchivingMetaData()
        {

        }
        #endregion
        #region 存档与缓存
        /// <summary>
        /// 将object写入缓存
        /// </summary>
        /// <param name="obj">要写入的obj</param>
        /// <param name="saveID">存档ID</param>
        /// <param name="saveName">存档名</param>
        public void SaveCache(object obj,int saveID,string saveName)
        {
            if(saveCache.ContainsKey(saveID))
            {
                if (saveCache[saveID].ContainsKey(saveName))
                {
                    saveCache[saveID][saveName] = obj;
                }
                else
                {
                    saveCache[saveID].Add(saveName, obj);
                }
            }
            else
            {
                saveCache.Add(saveID,new Dictionary<string, object>() { { saveName, obj } });
            }
        }
        public T LoadCache<T>(int saveId,string saveName) where T:class
        {
            if (saveCache.ContainsKey(saveId))
            {
                if (saveCache[saveId].ContainsKey(saveName))
                {
                    return saveCache[saveId][saveName] as T;
                }
                else
                    return null;
            }
            else
                return null;
        }
        #endregion
        #region 对象与存档
        /// <summary>
        /// 将对象存入具体的存档
        /// </summary>
        /// <param name="obj">需要存储的对象</param>
        /// <param name="saveID">存储的存档路径</param>
        /// <param name="saveFileName">存储的存档文件名</param>
        public void SaveArchiving(object obj,int saveID,string saveFileName)
        {
            string actualArchivingPath = GetOneArchivingPath(saveID,true);
            string finalPath = actualArchivingPath + "/" + saveFileName;
            SaveFile(obj,finalPath);
            SaveCache(obj,saveID,saveFileName);
            archivingMetaData.archivingItems[saveID].LastUpdateTime = DateTime.Now;
        }
        /// <summary>
        /// 将对象存入具体的存档
        /// </summary>
        /// <param name="obj">需要存储的对象</param>
        /// <param name="archivingItem">存档对象</param>
        /// <param name="saveFileName">存储的存档文件名</param>
        public void SaveArchiving(object obj, ArchivingItem archivingItem, string saveFileName)
        {
            string actualArchivingPath = GetOneArchivingPath(archivingItem.archivingId, true);
            string finalPath = actualArchivingPath + "/" + saveFileName;
            SaveFile(obj, finalPath);
            SaveCache(obj, archivingItem.archivingId, saveFileName);
            archivingItem.LastUpdateTime = DateTime.Now;
        }
        /// <summary>
        /// 将对象存入具体的存档（这里采用文件名为对象类型名的方式）
        /// </summary>
        /// <param name="obj">需要存储的对象</param>
        /// <param name="saveID">存档ID</param>
        public void SaveArchiving(object obj, int saveID = 0)
        {
            SaveArchiving(obj, saveID,obj.GetType().Name);
        }
        public void SaveArchiving(object obj, ArchivingItem archivingItem)
        {
            SaveArchiving(obj, archivingItem, obj.GetType().Name);
        }
        /// <summary>
        /// 将存档文件加载成对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="saveID">存档路径ID</param>
        /// <param name="saveFileName">存档文件名</param>
        /// <returns></returns>
        public T LoadArchiving<T>(int saveID, string saveFileName) where T : class
        {
            T obj = null;
            obj = LoadCache<T>(saveID, saveFileName);
            if (obj == null)
            {
                string actualArchivingPath = GetOneArchivingPath(saveID);
                if (actualArchivingPath == null) return null;
                string finalPath = actualArchivingPath + "/" + saveFileName;

                obj = LoadFile<T>(finalPath);
                SaveCache(obj, saveID, saveFileName);
            }

            return obj;
        }
        /// <summary>
        /// 将存档文件加载为对象（这里默认使用对象类型名作为存档文件名）
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="saveID">存档路径ID</param>
        /// <returns></returns>
        public T LoadArchiving<T>(int saveID=0) where T : class
        {
            return LoadArchiving<T>(saveID,typeof(T).Name);
        }
        //第二个参数为如果不存在是否创建存档路径,默认不创建
        public string GetOneArchivingPath(int saveID,bool isCreate = false) 
        {
            string actualArchivingPath = archivingPath + "/" + saveID.ToString();
            if(!Directory.Exists(actualArchivingPath))
            {
                if(isCreate)
                {
                    Directory.CreateDirectory(actualArchivingPath);
                }
                else
                {
                    return null;
                }
                
            }
            return actualArchivingPath;
        }
        #endregion
        #region 对象与文件
        public void SaveFile(object obj,string path)
        {

            using(FileStream f = new FileStream(path,FileMode.OpenOrCreate))
            {
                bf.Serialize(f, obj);
            }
        }

        public T LoadFile<T>(string path) where T :class
        {
            T obj = null;
            if(File.Exists(path))
            {
                using (FileStream f = new FileStream(path, FileMode.OpenOrCreate))
                {
                    obj = bf.Deserialize(f) as T;
                }
                    
            }
            return obj;
        }
        #endregion
    }
}

