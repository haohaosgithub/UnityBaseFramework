using Framework;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : ManagerBase<AudioManager>
{
    public AudioSource musicAudioSource;
    public List<AudioSource> effectAudioSourceList;
    public GameObject effectAudioSourcePrefab;
    private GameObject effectAudioSoureceGoRoot;

    [SerializeField]
    [Range(0,1)]
    [OnValueChanged("UpdateMusicAndEffectAuidioVolume")]
    private float globalVolume;
    public float GlobalVolume 
    { 
        get { return globalVolume; }
        set 
        { 
            if(globalVolume != value)
            {
                globalVolume = value;
                UpdateMusicVolume();
                UpdateEffectAudioVolume();
            }
            
        }
    }
    [SerializeField]
    [Range(0, 1)]
    [OnValueChanged("UpdateMusicVolume")]
    private float musicVolume; 
    public float MusicVolume
    {
        get { return musicVolume; }
        set 
        {
            if(musicVolume != value)
            {
                musicVolume = value;
                UpdateMusicVolume();
            }
            
        }
    }
    [SerializeField]
    [Range(0, 1)]
    [OnValueChanged("UpdateEffectAudioVolume")]
    private float effectAudioVolume;
    public float EffectAudioVolume
    {
        get { return effectAudioVolume; }
        set 
        {
            if(effectAudioVolume != value) 
            {
                effectAudioVolume = value;
                UpdateEffectAudioVolume();
            }
            
        }
    }
    [SerializeField]
    private bool isMute;
    public bool IsMute
    {
        get => isMute;
        set
        {
            if (isMute != value)
            {
                isMute = value;
                musicAudioSource.mute = isMute; 
                UpdateEffectAudioVolume();
            }
        }
    }

    [SerializeField]
    private bool isLoop;
    public bool IsLoop
    {
        get => isLoop;
        set
        {
            if (isLoop != value)
            {
                isLoop = value;
                musicAudioSource.loop = isLoop;
                //UpdateEffectAudioVolume();
            }
        }
    }
    
    [SerializeField]
    private bool isPause;
    public bool IsPause
    {
        get => isPause;
        set
        {
            if (isPause != value)
            {
                isPause = value;
                if(isPause)
                {
                    musicAudioSource.Pause();
                }
                else
                {
                    musicAudioSource.UnPause();
                }                
                UpdateEffectAudioVolume();
            }
        }
    }

    public override void Init()
    {
        base.Init();
        GlobalVolume = 1;
        MusicVolume = 1;
        EffectAudioVolume = 1;
    }

    //更新背景音乐大小
    public void UpdateMusicVolume()
    {
        musicAudioSource.volume = GlobalVolume * MusicVolume;
    }

    //音效管理器音效数值变化时更新所有音效组件的数值
    public void UpdateEffectAudioVolume()
    {
        for(int i = effectAudioSourceList.Count - 1;i>-1;i--) 
        {
            if (effectAudioSourceList[i] == null) //无效的音效对象直接从列表移除
            {
                effectAudioSourceList.RemoveAt(i);
            }
            else
            {
                SetNumOfEffectAudio(effectAudioSourceList[i]);
            }
        }
    }
    //更新指定音效组件的数值
    public void SetNumOfEffectAudio(AudioSource audioSource,int spatial = -1)
    {
        audioSource.mute = isMute;  
        audioSource.volume = EffectAudioVolume * GlobalVolume;       
        if (spatial != -1)  //不设置则为默认，否则设置
        {
            audioSource.spatialBlend = spatial;
        }
        if (isPause)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }

    }

    public void UpdateMusicAndEffectAuidioVolume()
    {
        UpdateMusicVolume();
        UpdateEffectAudioVolume();
    }

    
    
    public void PlayBGMusic(AudioClip clip, float volume = -1, bool isLoop = true)
    {
        //获取musicAudioSource组件并设置相关数据
        musicAudioSource.clip = clip;
        if(volume != -1)
        {
            MusicVolume = volume;
        }
        IsLoop = isLoop;
        
        //播放
        musicAudioSource.Play();
    }

    public void PlayBGMusic(string clipPath, float volume = 1, bool isLoop = true)
    {
        AudioClip clip = ResManager.Instance.LoadAsset<AudioClip>(clipPath);
        if(clip == null)
        {
            throw new System.Exception("音乐路径错误： " + clipPath);
        }
        PlayBGMusic(clip, volume, isLoop);
    }

    public void PlayOneShot(AudioClip clip, Component component,bool is3D = false, Action cb = null, int time = 0)
    {
        //获取音效组件并设置
        AudioSource audioSource = GetEffectAudioSource(is3D);
        audioSource.transform.SetParent(component.transform); 
        
        audioSource.transform.localPosition = Vector3.zero;
        //播放
        audioSource.PlayOneShot(clip);

        //回收音效对象
        RecycleAudioSourceGo(audioSource, clip, cb, time);
    }
    public void PlayOneShot(string clipPath, Component component, bool is3D = false, Action cb = null, int time = 0)
    {
        AudioClip clip = ResManager.Instance.LoadAsset<AudioClip>(clipPath);
        PlayOneShot(clip,component,is3D, cb, time);
    }
    public void PlayOneShot(AudioClip clip, Vector3 positon,bool is3D = false, Action cb = null, int time = 0)
    {
        //获取音效组件并设置相关数据
        AudioSource audioSource = GetEffectAudioSource(is3D);
        audioSource.transform.position = Vector3.zero;
        //播放
        audioSource.PlayOneShot(clip);

        //回收音效对象
        RecycleAudioSourceGo(audioSource, clip, cb, time);
    }
    public void PlayOneShot(string clipPath, Vector3 positon, bool is3D = false, Action cb = null, int time = 0)
    {
        AudioClip clip = ResManager.Instance.LoadAsset<AudioClip>(clipPath);
        PlayOneShot(clip, positon, is3D, cb, time);
    }


    public AudioSource GetEffectAudioSource(bool is3D = false)
    {
        if (effectAudioSoureceGoRoot == null)
        {
            effectAudioSoureceGoRoot = new GameObject("EffectAudioSoureceGoRoot");
        }
        //之所以不从ResManager中获取是因为AudioSource脚本组件无法添加Pool特性，只能走非对象池逻辑
        //除非修改实现方式为手动添加，否则原生的脚本组件如果想要用对象池的逻辑只能直接用对象池管理器
        //ResManager.Instance.Load<AudioSource>("Audio/EffectAudioSource");
        //AudioSource audioSource = PoolManager.Instance.GetGameObject<AudioSource>(effectAudioSourcePrefab);
        AudioSource audioSource = PoolManager.Instance.GetGameObject<AudioSource>(effectAudioSourcePrefab,effectAudioSoureceGoRoot.transform);
        SetNumOfEffectAudio(audioSource,is3D? 1: 0);
        if (!effectAudioSourceList.Contains(audioSource)) //相同音效对象不重复添加，后续可考虑调整为hashset存储
        {
            effectAudioSourceList.Add(audioSource);
        }
        return audioSource;
    }
    
    public void RecycleAudioSourceGo(AudioSource audioSource,AudioClip clip,Action cb,int time)
    {
        StartCoroutine(DoRecycleAudioSourceGo(audioSource,clip,cb,time));
    }
    /// <summary>
    /// 回收音效对象的协程
    /// </summary>
    /// <param name="audioSource">音效源</param>
    /// <param name="clip">音效切片</param>
    /// <param name="cb">音效播放完成后，再间隔time的callback</param>
    /// <param name="time">间隔时间</param>
    /// <returns></returns>
    public IEnumerator DoRecycleAudioSourceGo(AudioSource audioSource, AudioClip clip, Action cb, int time)
    {

        yield return new WaitForSeconds(clip.length);
        if(audioSource !=null)
        {
            PoolManager.Instance.PushGameObj(audioSource.gameObject);
        }
        yield return new WaitForSeconds(time);
        cb?.Invoke();
    }
}
