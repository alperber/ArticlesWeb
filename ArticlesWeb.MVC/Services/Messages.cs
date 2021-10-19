using System;
using System.Collections.Generic;
using System.Text;

namespace ArticlesWeb.MVC.Services
{
    public static class Messages
    {
        public static string RegisterSuccesful = "Başarıyla Kaydedildi";
        public static string WrongInput = "Kullanıcı Adı veya Şifre Yanlış";
        public static string PostCreated = "Post Başarıyla Eklendi";
        public static string UserDeleted = "Kullanıcı Başarıyla Silindi";
        public static string UserDoesntExists = "Kullanıcı Bulunamadı";
        public static string PostDoesntExists = "Post Bulunamadı";
        public static string PostDeleted = "Post Başarıyla Silindi";
        public static string UserDoesntHavePost = "Kullanıcıya Ait Post Bulunamadı";
        public static string EmailAlreadyExists = "Bu Email Zaten Kullanılıyor";
        public static string UsernameAlreadyExists = "Bu Kullanıcı Adı Zaten Kullanılıyor";
        public static string CommentDoesntExists = "Böyle bir yorum yok";
        public static string UserAlreadyAdmin = "Kullanıcı Zaten Admin";
    }
}