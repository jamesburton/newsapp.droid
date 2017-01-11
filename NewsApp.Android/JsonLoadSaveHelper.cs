using PCLStorage;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NewsApp.Android
{
    public static class JsonLoadSaveHelper
    {
        const string FolderName = "JsonData";
        static readonly IFolder Folder;
        static JsonLoadSaveHelper() {
            var rootFolder = FileSystem.Current.LocalStorage;
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Initialising JsonLoadSaveHelper: rootFolder=" + rootFolder.Path);
#endif
            Folder = rootFolder.CreateFolderAsync("JsonData", CreationCollisionOption.OpenIfExists).ConfigureAwait(true).GetAwaiter().GetResult();
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Initialised JsonLoadSaveHelper: Folder=" + Folder.Path);
#endif
        }
        public static async Task<bool> SaveJson(this object objectToSave, string name)
        {
            var file = await Folder.CreateFileAsync(name + ".json",
                CreationCollisionOption.ReplaceExisting);
#if DEBUG
            System.Diagnostics.Debug.WriteLine("JsonLoadSaveHelper saving json: name=" + name);
#endif
            var json = JsonConvert.SerializeObject(objectToSave);
            await file.WriteAllTextAsync(json);
#if DEBUG
            System.Diagnostics.Debug.WriteLine("JsonLoadSaveHelper saved json: json=" + json);
#endif
            return true;
        }

        public static async Task<T> LoadJson<T>(this string name)
            where T : class
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("JsonLoadSaveHelper loading json: name=" + name);
#endif
            var file = await Folder.CreateFileAsync(name + ".json", CreationCollisionOption.OpenIfExists);
            var json = await file.ReadAllTextAsync();
#if DEBUG
            System.Diagnostics.Debug.WriteLine("JsonLoadSaveHelper loaded json: json=" + json);
#endif
            return string.IsNullOrEmpty(json) ? null : await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(json));
        }
    }
}