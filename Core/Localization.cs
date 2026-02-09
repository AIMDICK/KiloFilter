using System;
using System.Collections.Generic;

namespace KiloFilter.Core
{
    public static class Localization
    {
        public enum Language
        {
            English,
            Spanish,
            French,
            German,
            Italian,
            Japanese
        }

        public static Language CurrentLanguage { get; set; } = Language.English;

        private static Dictionary<string, Dictionary<Language, string>> translations = new Dictionary<string, Dictionary<Language, string>>
        {
            // Ventana principal
            { "APP_TITLE", new Dictionary<Language, string> {
                { Language.English, "KiloFilter - Smart File Organization" },
                { Language.Spanish, "KiloFilter - Organización Inteligente de Archivos" },
                { Language.French, "KiloFilter - Organisation Intelligente des Fichiers" },
                { Language.German, "KiloFilter - Intelligente Dateiorganisation" },
                { Language.Italian, "KiloFilter - Organizzazione Intelligente dei File" },
                { Language.Japanese, "KiloFilter - スマートファイル整理" }
            }},
            { "SOURCE_FOLDER", new Dictionary<Language, string> {
                { Language.English, "SOURCE FOLDER:" },
                { Language.Spanish, "CARPETA DE ORIGEN:" },
                { Language.French, "DOSSIER SOURCE:" },
                { Language.German, "QUELLORDNER:" },
                { Language.Italian, "CARTELLA SORGENTE:" },
                { Language.Japanese, "ソースフォルダー:" }
            }},
            { "DESTINATION_FOLDER", new Dictionary<Language, string> {
                { Language.English, "DESTINATION FOLDER:" },
                { Language.Spanish, "CARPETA DE DESTINO:" },
                { Language.French, "DOSSIER DE DESTINATION:" },
                { Language.German, "ZIELORDNER:" },
                { Language.Italian, "CARTELLA DI DESTINAZIONE:" },
                { Language.Japanese, "保存先フォルダー:" }
            }},
            { "BTN_BROWSE", new Dictionary<Language, string> {
                { Language.English, "Browse..." },
                { Language.Spanish, "Examinar..." },
                { Language.French, "Parcourir..." },
                { Language.German, "Durchsuchen..." },
                { Language.Italian, "Sfoglia..." },
                { Language.Japanese, "参照..." }
            }},
            { "BTN_CLEAR", new Dictionary<Language, string> {
                { Language.English, "CLEAR" },
                { Language.Spanish, "LIMPIAR" },
                { Language.French, "EFFACER" },
                { Language.German, "LÖSCHEN" },
                { Language.Italian, "PULISCI" },
                { Language.Japanese, "クリア" }
            }},
            { "BTN_ANALYZE", new Dictionary<Language, string> {
                { Language.English, "1. ANALYZE DISK" },
                { Language.Spanish, "1. ANALIZAR DISCO" },
                { Language.French, "1. ANALYSER LE DISQUE" },
                { Language.German, "1. FESTPLATTE ANALYSIEREN" },
                { Language.Italian, "1. ANALIZZA DISCO" },
                { Language.Japanese, "1. ディスクを分析" }
            }},
            { "BTN_CONFIGURE", new Dictionary<Language, string> {
                { Language.English, "⚙ Configure" },
                { Language.Spanish, "⚙ Configurar" },
                { Language.French, "⚙ Configurer" },
                { Language.German, "⚙ Konfigurieren" },
                { Language.Italian, "⚙ Configura" },
                { Language.Japanese, "⚙ 設定" }
            }},
            { "BTN_NEW_CATEGORY", new Dictionary<Language, string> {
                { Language.English, "➕ New Category" },
                { Language.Spanish, "➕ Nueva Categoría" },
                { Language.French, "➕ Nouvelle Catégorie" },
                { Language.German, "➕ Neue Kategorie" },
                { Language.Italian, "➕ Nuova Categoria" },
                { Language.Japanese, "➕ 新しいカテゴリー" }
            }},
            { "BTN_RESCUE", new Dictionary<Language, string> {
                { Language.English, "2. RESCUE SELECTED" },
                { Language.Spanish, "2. RESCATAR SELECCIONADOS" },
                { Language.French, "2. SAUVEGARDER SÉLECTIONNÉS" },
                { Language.German, "2. AUSGEWÄHLTE RETTEN" },
                { Language.Italian, "2. SALVA SELEZIONATI" },
                { Language.Japanese, "2. 選択したものを救出" }
            }},
            { "STATUS_READY", new Dictionary<Language, string> {
                { Language.English, "Ready" },
                { Language.Spanish, "Listo" },
                { Language.French, "Prêt" },
                { Language.German, "Bereit" },
                { Language.Italian, "Pronto" },
                { Language.Japanese, "準備完了" }
            }},
            { "COL_INCLUDE", new Dictionary<Language, string> {
                { Language.English, "Include?" },
                { Language.Spanish, "¿Incluir?" },
                { Language.French, "Inclure?" },
                { Language.German, "Einschließen?" },
                { Language.Italian, "Includere?" },
                { Language.Japanese, "含める？" }
            }},
            { "COL_CATEGORY", new Dictionary<Language, string> {
                { Language.English, "Category" },
                { Language.Spanish, "Categoría" },
                { Language.French, "Catégorie" },
                { Language.German, "Kategorie" },
                { Language.Italian, "Categoria" },
                { Language.Japanese, "カテゴリー" }
            }},
            { "COL_FILES", new Dictionary<Language, string> {
                { Language.English, "Files" },
                { Language.Spanish, "Archivos" },
                { Language.French, "Fichiers" },
                { Language.German, "Dateien" },
                { Language.Italian, "File" },
                { Language.Japanese, "ファイル" }
            }},
            { "COL_SIZE", new Dictionary<Language, string> {
                { Language.English, "Size" },
                { Language.Spanish, "Peso" },
                { Language.French, "Taille" },
                { Language.German, "Größe" },
                { Language.Italian, "Dimensione" },
                { Language.Japanese, "サイズ" }
            }},
            { "BTN_LANGUAGE", new Dictionary<Language, string> {
                { Language.English, "🌐 Language" },
                { Language.Spanish, "🌐 Idioma" },
                { Language.French, "🌐 Langue" },
                { Language.German, "🌐 Sprache" },
                { Language.Italian, "🌐 Lingua" },
                { Language.Japanese, "🌐 言語" }
            }},
            { "BTN_VIEW_DETAILS", new Dictionary<Language, string> {
                { Language.English, "🔍 View Details" },
                { Language.Spanish, "🔍 Ver Detalle" },
                { Language.French, "🔍 Voir Détails" },
                { Language.German, "🔍 Details Anzeigen" },
                { Language.Italian, "🔍 Visualizza Dettagli" },
                { Language.Japanese, "🔍 詳細を表示" }
            }},
            
            // Nombres de categorías
            { "CAT_IMAGES", new Dictionary<Language, string> {
                { Language.English, "Images" },
                { Language.Spanish, "Imágenes" },
                { Language.French, "Images" },
                { Language.German, "Bilder" },
                { Language.Italian, "Immagini" },
                { Language.Japanese, "画像" }
            }},
            { "CAT_VIDEOS", new Dictionary<Language, string> {
                { Language.English, "Videos" },
                { Language.Spanish, "Videos" },
                { Language.French, "Vidéos" },
                { Language.German, "Videos" },
                { Language.Italian, "Video" },
                { Language.Japanese, "動画" }
            }},
            { "CAT_DOCUMENTS", new Dictionary<Language, string> {
                { Language.English, "Documents" },
                { Language.Spanish, "Documentos" },
                { Language.French, "Documents" },
                { Language.German, "Dokumente" },
                { Language.Italian, "Documenti" },
                { Language.Japanese, "ドキュメント" }
            }},
            { "CAT_AUDIO", new Dictionary<Language, string> {
                { Language.English, "Audio" },
                { Language.Spanish, "Audio" },
                { Language.French, "Audio" },
                { Language.German, "Audio" },
                { Language.Italian, "Audio" },
                { Language.Japanese, "オーディオ" }
            }},
            { "CAT_COMPRESSED", new Dictionary<Language, string> {
                { Language.English, "Compressed" },
                { Language.Spanish, "Comprimidos" },
                { Language.French, "Compressés" },
                { Language.German, "Komprimiert" },
                { Language.Italian, "Compressi" },
                { Language.Japanese, "圧縮" }
            }},
            { "CAT_GAMES", new Dictionary<Language, string> {
                { Language.English, "Games & Worlds" },
                { Language.Spanish, "Juegos y Mundos" },
                { Language.French, "Jeux et Mondes" },
                { Language.German, "Spiele & Welten" },
                { Language.Italian, "Giochi e Mondi" },
                { Language.Japanese, "ゲームとワールド" }
            }},
            { "CAT_APPS", new Dictionary<Language, string> {
                { Language.English, "Applications APK" },
                { Language.Spanish, "Aplicaciones APK" },
                { Language.French, "Applications APK" },
                { Language.German, "Anwendungen APK" },
                { Language.Italian, "Applicazioni APK" },
                { Language.Japanese, "アプリ APK" }
            }},
            { "CAT_DATABASES", new Dictionary<Language, string> {
                { Language.English, "Databases" },
                { Language.Spanish, "Bases de Datos" },
                { Language.French, "Bases de Données" },
                { Language.German, "Datenbanken" },
                { Language.Italian, "Database" },
                { Language.Japanese, "データベース" }
            }},
            { "CAT_SOURCE_CODE", new Dictionary<Language, string> {
                { Language.English, "Source Code" },
                { Language.Spanish, "Código Fuente" },
                { Language.French, "Code Source" },
                { Language.German, "Quellcode" },
                { Language.Italian, "Codice Sorgente" },
                { Language.Japanese, "ソースコード" }
            }},
            { "CAT_3D_MODELS", new Dictionary<Language, string> {
                { Language.English, "3D Models" },
                { Language.Spanish, "Modelos 3D" },
                { Language.French, "Modèles 3D" },
                { Language.German, "3D-Modelle" },
                { Language.Italian, "Modelli 3D" },
                { Language.Japanese, "3Dモデル" }
            }},
            { "CAT_EBOOKS", new Dictionary<Language, string> {
                { Language.English, "Ebooks" },
                { Language.Spanish, "Ebooks" },
                { Language.French, "Livres numériques" },
                { Language.German, "E-Books" },
                { Language.Italian, "Ebook" },
                { Language.Japanese, "電子書籍" }
            }},
            { "CAT_SUBTITLES", new Dictionary<Language, string> {
                { Language.English, "Subtitles" },
                { Language.Spanish, "Subtítulos" },
                { Language.French, "Sous-titres" },
                { Language.German, "Untertitel" },
                { Language.Italian, "Sottotitoli" },
                { Language.Japanese, "字幕" }
            }},
            { "CAT_OTHERS", new Dictionary<Language, string> {
                { Language.English, "Others" },
                { Language.Spanish, "Lo Demás" },
                { Language.French, "Autres" },
                { Language.German, "Sonstiges" },
                { Language.Italian, "Altri" },
                { Language.Japanese, "その他" }
            }},
            
            // Ventana de nueva categoría
            { "NEW_CATEGORY_TITLE", new Dictionary<Language, string> {
                { Language.English, "CREATE NEW CUSTOM CATEGORY" },
                { Language.Spanish, "CREAR NUEVA CATEGORÍA PERSONALIZADA" },
                { Language.French, "CRÉER UNE NOUVELLE CATÉGORIE PERSONNALISÉE" },
                { Language.German, "NEUE BENUTZERDEFINIERTE KATEGORIE ERSTELLEN" },
                { Language.Italian, "CREA NUOVA CATEGORIA PERSONALIZZATA" },
                { Language.Japanese, "新しいカスタムカテゴリーを作成" }
            }},
            { "CATEGORY_NAME", new Dictionary<Language, string> {
                { Language.English, "Category name" },
                { Language.Spanish, "Nombre de la categoría" },
                { Language.French, "Nom de la catégorie" },
                { Language.German, "Kategoriename" },
                { Language.Italian, "Nome della categoria" },
                { Language.Japanese, "カテゴリー名" }
            }},
            { "EXAMPLE", new Dictionary<Language, string> {
                { Language.English, "Example: Programming, GraphicDesign, ElectronicMusic" },
                { Language.Spanish, "Ejemplo: Programacion, DiseñoGrafico, MusicaElectronica" },
                { Language.French, "Exemple: Programmation, DesignGraphique, MusiqueElectronique" },
                { Language.German, "Beispiel: Programmierung, Grafikdesign, ElektronischeMusik" },
                { Language.Italian, "Esempio: Programmazione, DesignGrafico, MusicaElettronica" },
                { Language.Japanese, "例: プログラミング、グラフィックデザイン、エレクトロニック音楽" }
            }},
            { "INCLUDED_EXTENSIONS", new Dictionary<Language, string> {
                { Language.English, "Included extensions" },
                { Language.Spanish, "Extensiones incluidas" },
                { Language.French, "Extensions incluses" },
                { Language.German, "Enthaltene Erweiterungen" },
                { Language.Italian, "Estensioni incluse" },
                { Language.Japanese, "含まれる拡張子" }
            }},
            { "BTN_REMOVE", new Dictionary<Language, string> {
                { Language.English, "Remove" },
                { Language.Spanish, "Eliminar" },
                { Language.French, "Supprimer" },
                { Language.German, "Entfernen" },
                { Language.Italian, "Rimuovi" },
                { Language.Japanese, "削除" }
            }},
            { "BTN_CLEAR_ALL", new Dictionary<Language, string> {
                { Language.English, "Clear All" },
                { Language.Spanish, "Limpiar Todo" },
                { Language.French, "Tout Effacer" },
                { Language.German, "Alles Löschen" },
                { Language.Italian, "Pulisci Tutto" },
                { Language.Japanese, "すべてクリア" }
            }},
            { "TOTAL_EXTENSIONS", new Dictionary<Language, string> {
                { Language.English, "Total: {0} extension(s)" },
                { Language.Spanish, "Total: {0} extensión(es)" },
                { Language.French, "Total: {0} extension(s)" },
                { Language.German, "Gesamt: {0} Erweiterung(en)" },
                { Language.Italian, "Totale: {0} estensione/i" },
                { Language.Japanese, "合計: {0} 拡張子" }
            }},
            { "ADD_EXTENSION", new Dictionary<Language, string> {
                { Language.English, "Add extension (e.g. .py, .psd, .mp3)" },
                { Language.Spanish, "Agregar extensión (ej: .py, .psd, .mp3)" },
                { Language.French, "Ajouter une extension (ex: .py, .psd, .mp3)" },
                { Language.German, "Erweiterung hinzufügen (z.B. .py, .psd, .mp3)" },
                { Language.Italian, "Aggiungi estensione (es: .py, .psd, .mp3)" },
                { Language.Japanese, "拡張子を追加 (例: .py, .psd, .mp3)" }
            }},
            { "BTN_ADD", new Dictionary<Language, string> {
                { Language.English, "Add" },
                { Language.Spanish, "Agregar" },
                { Language.French, "Ajouter" },
                { Language.German, "Hinzufügen" },
                { Language.Italian, "Aggiungi" },
                { Language.Japanese, "追加" }
            }},
            { "BTN_SAVE_AND_ANALYZE", new Dictionary<Language, string> {
                { Language.English, "Save and Analyze" },
                { Language.Spanish, "Guardar y Analizar" },
                { Language.French, "Enregistrer et Analyser" },
                { Language.German, "Speichern und Analysieren" },
                { Language.Italian, "Salva e Analizza" },
                { Language.Japanese, "保存して分析" }
            }},
            { "BTN_SAVE_ONLY", new Dictionary<Language, string> {
                { Language.English, "Save Only" },
                { Language.Spanish, "Solo Guardar" },
                { Language.French, "Enregistrer Seulement" },
                { Language.German, "Nur Speichern" },
                { Language.Italian, "Solo Salva" },
                { Language.Japanese, "保存のみ" }
            }},
            { "BTN_CANCEL", new Dictionary<Language, string> {
                { Language.English, "Cancel" },
                { Language.Spanish, "Cancelar" },
                { Language.French, "Annuler" },
                { Language.German, "Abbrechen" },
                { Language.Italian, "Annulla" },
                { Language.Japanese, "キャンセル" }
            }},

            // Ventana de detalles de categoría
            { "DETAILED_ANALYSIS", new Dictionary<Language, string> {
                { Language.English, "Detailed Analysis - {0}" },
                { Language.Spanish, "Análisis Detallado - {0}" },
                { Language.French, "Analyse Détaillée - {0}" },
                { Language.German, "Detaillierte Analyse - {0}" },
                { Language.Italian, "Analisi Dettagliata - {0}" },
                { Language.Japanese, "詳細分析 - {0}" }
            }},
            { "CATEGORY_INFO", new Dictionary<Language, string> {
                { Language.English, "Category: {0} | {1} extension types | {2} files | {3}" },
                { Language.Spanish, "Categoría: {0} | {1} tipos de extensiones | {2} archivos | {3}" },
                { Language.French, "Catégorie: {0} | {1} types d'extensions | {2} fichiers | {3}" },
                { Language.German, "Kategorie: {0} | {1} Erweiterungstypen | {2} Dateien | {3}" },
                { Language.Italian, "Categoria: {0} | {1} tipi di estensioni | {2} file | {3}" },
                { Language.Japanese, "カテゴリー: {0} | {1} 拡張子タイプ | {2} ファイル | {3}" }
            }},
            { "TAB_FILE_EXPLORER", new Dictionary<Language, string> {
                { Language.English, "File Explorer" },
                { Language.Spanish, "Explorador de Archivos" },
                { Language.French, "Explorateur de Fichiers" },
                { Language.German, "Datei-Explorer" },
                { Language.Italian, "Esplora File" },
                { Language.Japanese, "ファイルエクスプローラー" }
            }},
            { "TAB_SUMMARY", new Dictionary<Language, string> {
                { Language.English, "Summary by Extension" },
                { Language.Spanish, "Resumen por Extensión" },
                { Language.French, "Résumé par Extension" },
                { Language.German, "Zusammenfassung nach Erweiterung" },
                { Language.Italian, "Riepilogo per Estensione" },
                { Language.Japanese, "拡張子別サマリー" }
            }},
            { "BTN_APPLY_CHANGES", new Dictionary<Language, string> {
                { Language.English, "Apply Changes" },
                { Language.Spanish, "Aplicar Cambios" },
                { Language.French, "Appliquer les Modifications" },
                { Language.German, "Änderungen Übernehmen" },
                { Language.Italian, "Applica Modifiche" },
                { Language.Japanese, "変更を適用" }
            }},
            { "BTN_SAVE_AND_CLOSE", new Dictionary<Language, string> {
                { Language.English, "💾 Save and Close" },
                { Language.Spanish, "💾 Guardar y Cerrar" },
                { Language.French, "💾 Enregistrer et Fermer" },
                { Language.German, "💾 Speichern und Schließen" },
                { Language.Italian, "💾 Salva e Chiudi" },
                { Language.Japanese, "💾 保存して閉じる" }
            }},
            { "FILTER", new Dictionary<Language, string> {
                { Language.English, "Filter" },
                { Language.Spanish, "Filtrar" },
                { Language.French, "Filtrer" },
                { Language.German, "Filtern" },
                { Language.Italian, "Filtra" },
                { Language.Japanese, "フィルター" }
            }},
            { "BTN_COPY_SUMMARY", new Dictionary<Language, string> {
                { Language.English, "Copy Summary" },
                { Language.Spanish, "Copiar Resumen" },
                { Language.French, "Copier le Résumé" },
                { Language.German, "Zusammenfassung Kopieren" },
                { Language.Italian, "Copia Riepilogo" },
                { Language.Japanese, "サマリーをコピー" }
            }},
            { "COL_SELECTION", new Dictionary<Language, string> {
                { Language.English, "Sel" },
                { Language.Spanish, "Sel" },
                { Language.French, "Sél" },
                { Language.German, "Aus" },
                { Language.Italian, "Sel" },
                { Language.Japanese, "選択" }
            }},
            { "COL_EXTENSION", new Dictionary<Language, string> {
                { Language.English, "Extension" },
                { Language.Spanish, "Extensión" },
                { Language.French, "Extension" },
                { Language.German, "Erweiterung" },
                { Language.Italian, "Estensione" },
                { Language.Japanese, "拡張子" }
            }},
            { "COL_QUANTITY", new Dictionary<Language, string> {
                { Language.English, "Quantity" },
                { Language.Spanish, "Cantidad" },
                { Language.French, "Quantité" },
                { Language.German, "Anzahl" },
                { Language.Italian, "Quantità" },
                { Language.Japanese, "数量" }
            }},
            { "COL_TOTAL_SIZE", new Dictionary<Language, string> {
                { Language.English, "Total Size" },
                { Language.Spanish, "Tamaño Total" },
                { Language.French, "Taille Totale" },
                { Language.German, "Gesamtgröße" },
                { Language.Italian, "Dimensione Totale" },
                { Language.Japanese, "合計サイズ" }
            }},
            { "COL_AVERAGE_SIZE", new Dictionary<Language, string> {
                { Language.English, "Average Size" },
                { Language.Spanish, "Tamaño Promedio" },
                { Language.French, "Taille Moyenne" },
                { Language.German, "Durchschnittsgröße" },
                { Language.Italian, "Dimensione Media" },
                { Language.Japanese, "平均サイズ" }
            }},
            { "COL_PERCENTAGE", new Dictionary<Language, string> {
                { Language.English, "% of Total" },
                { Language.Spanish, "% del Total" },
                { Language.French, "% du Total" },
                { Language.German, "% der Gesamtmenge" },
                { Language.Italian, "% del Totale" },
                { Language.Japanese, "全体の%" }
            }},
            { "COL_LARGEST", new Dictionary<Language, string> {
                { Language.English, "Largest" },
                { Language.Spanish, "Más Grande" },
                { Language.French, "Plus Grand" },
                { Language.German, "Größte" },
                { Language.Italian, "Più Grande" },
                { Language.Japanese, "最大" }
            }},
            { "MOVE_TO", new Dictionary<Language, string> {
                { Language.English, "Move selected extensions to:" },
                { Language.Spanish, "Mover extensiones seleccionadas a:" },
                { Language.French, "Déplacer les extensions sélectionnées vers:" },
                { Language.German, "Ausgewählte Erweiterungen verschieben nach:" },
                { Language.Italian, "Sposta le estensioni selezionate in:" },
                { Language.Japanese, "選択した拡張子を移動:" }
            }},
            { "BTN_MOVE", new Dictionary<Language, string> {
                { Language.English, "MOVE" },
                { Language.Spanish, "MOVER" },
                { Language.French, "DÉPLACER" },
                { Language.German, "VERSCHIEBEN" },
                { Language.Italian, "SPOSTA" },
                { Language.Japanese, "移動" }
            }},
            { "BTN_SELECT_ALL", new Dictionary<Language, string> {
                { Language.English, "Select All" },
                { Language.Spanish, "Marcar Todas" },
                { Language.French, "Tout Sélectionner" },
                { Language.German, "Alle Auswählen" },
                { Language.Italian, "Seleziona Tutto" },
                { Language.Japanese, "すべて選択" }
            }},
            { "BTN_DESELECT_ALL", new Dictionary<Language, string> {
                { Language.English, "❌ Deselect" },
                { Language.Spanish, "❌ Desmarcar" },
                { Language.French, "❌ Désélectionner" },
                { Language.German, "❌ Abwählen" },
                { Language.Italian, "❌ Deseleziona" },
                { Language.Japanese, "❌ 選択解除" }
            }},
            { "SELECTED_COUNT", new Dictionary<Language, string> {
                { Language.English, "Selected: {0}" },
                { Language.Spanish, "Seleccionadas: {0}" },
                { Language.French, "Sélectionnées: {0}" },
                { Language.German, "Ausgewählt: {0}" },
                { Language.Italian, "Selezionate: {0}" },
                { Language.Japanese, "選択済み: {0}" }
            }},
            { "COL_NAME", new Dictionary<Language, string> {
                { Language.English, "Name" },
                { Language.Spanish, "Nombre" },
                { Language.French, "Nom" },
                { Language.German, "Name" },
                { Language.Italian, "Nome" },
                { Language.Japanese, "名前" }
            }},
            { "COL_MODIFIED_DATE", new Dictionary<Language, string> {
                { Language.English, "Modified Date" },
                { Language.Spanish, "Fecha Modificación" },
                { Language.French, "Date de Modification" },
                { Language.German, "Änderungsdatum" },
                { Language.Italian, "Data Modifica" },
                { Language.Japanese, "変更日" }
            }},
            { "COL_CREATION_DATE", new Dictionary<Language, string> {
                { Language.English, "Creation Date" },
                { Language.Spanish, "Fecha Creación" },
                { Language.French, "Date de Création" },
                { Language.German, "Erstellungsdatum" },
                { Language.Italian, "Data Creazione" },
                { Language.Japanese, "作成日" }
            }},
            { "COL_FOLDER", new Dictionary<Language, string> {
                { Language.English, "Folder" },
                { Language.Spanish, "Carpeta" },
                { Language.French, "Dossier" },
                { Language.German, "Ordner" },
                { Language.Italian, "Cartella" },
                { Language.Japanese, "フォルダー" }
            }},

            // Ventana de configuración
            { "CONFIG_TITLE", new Dictionary<Language, string> {
                { Language.English, "Configure Extensions and Blacklist" },
                { Language.Spanish, "Configurar Extensiones y Blacklist" },
                { Language.French, "Configurer les Extensions et la Liste Noire" },
                { Language.German, "Erweiterungen und Blacklist Konfigurieren" },
                { Language.Italian, "Configura Estensioni e Blacklist" },
                { Language.Japanese, "拡張子とブラックリストを設定" }
            }},
            { "BTN_RESET", new Dictionary<Language, string> {
                { Language.English, "🔄 Reset" },
                { Language.Spanish, "🔄 Restablecer" },
                { Language.French, "🔄 Réinitialiser" },
                { Language.German, "🔄 Zurücksetzen" },
                { Language.Italian, "🔄 Ripristina" },
                { Language.Japanese, "🔄 リセット" }
            }},
            { "BTN_APPLY", new Dictionary<Language, string> {
                { Language.English, "✓ Apply" },
                { Language.Spanish, "✓ Aplicar" },
                { Language.French, "✓ Appliquer" },
                { Language.German, "✓ Übernehmen" },
                { Language.Italian, "✓ Applica" },
                { Language.Japanese, "✓ 適用" }
            }},
            { "BTN_OK", new Dictionary<Language, string> {
                { Language.English, "💾 Save and Close" },
                { Language.Spanish, "💾 Guardar y Cerrar" },
                { Language.French, "💾 Enregistrer et Fermer" },
                { Language.German, "💾 Speichern und Schließen" },
                { Language.Italian, "💾 Salva e Chiudi" },
                { Language.Japanese, "💾 保存して閉じる" }
            }},
            { "BTN_CONFIG_MIN_SIZE", new Dictionary<Language, string> {
                { Language.English, "🔧📏 Configure\nMin Size" },
                { Language.Spanish, "🔧📏 Configurar\nTamaño Mínimo" },
                { Language.French, "🔧📏 Configurer\nTaille Min" },
                { Language.German, "🔧📏 Konfigurieren\nMin. Größe" },
                { Language.Italian, "🔧📏 Configura\nDim. Minima" },
                { Language.Japanese, "🔧📏 設定\n最小サイズ" }
            }},
            { "TAB_BLACKLIST", new Dictionary<Language, string> {
                { Language.English, "⛔ BLACKLIST" },
                { Language.Spanish, "⛔ BLACKLIST" },
                { Language.French, "⛔ LISTE NOIRE" },
                { Language.German, "⛔ BLACKLIST" },
                { Language.Italian, "⛔ BLACKLIST" },
                { Language.Japanese, "⛔ ブラックリスト" }
            }},
            { "BLOCKED_EXTENSIONS_LABEL", new Dictionary<Language, string> {
                { Language.English, "Blocked extensions (check to remove from blacklist):" },
                { Language.Spanish, "Extensiones bloqueadas (marca para eliminar del bloqueo):" },
                { Language.French, "Extensions bloquées (cocher pour retirer de la liste noire):" },
                { Language.German, "Blockierte Erweiterungen (markieren zum Entfernen):" },
                { Language.Italian, "Estensioni bloccate (spunta per rimuovere dal blocco):" },
                { Language.Japanese, "ブロックされた拡張子（チェックしてブロック解除）:" }
            }},
            { "ADD_TO_BLACKLIST_LABEL", new Dictionary<Language, string> {
                { Language.English, "Add to blacklist (e.g.: .log or log):" },
                { Language.Spanish, "Agregar a blacklist (ej: .log o log):" },
                { Language.French, "Ajouter à la liste noire (ex: .log ou log):" },
                { Language.German, "Zur Blacklist hinzufügen (z.B.: .log oder log):" },
                { Language.Italian, "Aggiungi alla blacklist (es: .log o log):" },
                { Language.Japanese, "ブラックリストに追加（例: .log または log）:" }
            }},
            { "BTN_REMOVE_FROM_BLACKLIST", new Dictionary<Language, string> {
                { Language.English, "Remove from Blacklist" },
                { Language.Spanish, "Quitar de Blacklist" },
                { Language.French, "Retirer de la Liste Noire" },
                { Language.German, "Von Blacklist Entfernen" },
                { Language.Italian, "Rimuovi dalla Blacklist" },
                { Language.Japanese, "ブラックリストから削除" }
            }},
            { "BTN_BLOCK", new Dictionary<Language, string> {
                { Language.English, "Block" },
                { Language.Spanish, "Bloquear" },
                { Language.French, "Bloquer" },
                { Language.German, "Blockieren" },
                { Language.Italian, "Blocca" },
                { Language.Japanese, "ブロック" }
            }},
            { "BTN_CHECK_ALL", new Dictionary<Language, string> {
                { Language.English, "Check All" },
                { Language.Spanish, "Marcar Todas" },
                { Language.French, "Tout Cocher" },
                { Language.German, "Alle Markieren" },
                { Language.Italian, "Seleziona Tutto" },
                { Language.Japanese, "すべて選択" }
            }},
            { "BTN_UNCHECK_ALL", new Dictionary<Language, string> {
                { Language.English, "Uncheck All" },
                { Language.Spanish, "Desmarcar" },
                { Language.French, "Tout Décocher" },
                { Language.German, "Alle Abwählen" },
                { Language.Italian, "Deseleziona Tutto" },
                { Language.Japanese, "すべて解除" }
            }},
            { "TOTAL_BLOCKED", new Dictionary<Language, string> {
                { Language.English, "Total blocked: {0}" },
                { Language.Spanish, "Total bloqueadas: {0}" },
                { Language.French, "Total bloquées: {0}" },
                { Language.German, "Gesamt blockiert: {0}" },
                { Language.Italian, "Totale bloccate: {0}" },
                { Language.Japanese, "合計ブロック: {0}" }
            }},
            { "CATEGORY_EXTENSIONS_LABEL", new Dictionary<Language, string> {
                { Language.English, "Category extensions (check to remove):" },
                { Language.Spanish, "Extensiones de categoría (marca para eliminar):" },
                { Language.French, "Extensions de catégorie (cocher pour supprimer):" },
                { Language.German, "Kategorie-Erweiterungen (markieren zum Entfernen):" },
                { Language.Italian, "Estensioni di categoria (spunta per rimuovere):" },
                { Language.Japanese, "カテゴリー拡張子（チェックして削除）:" }
            }},
            { "ADD_EXTENSION_LABEL", new Dictionary<Language, string> {
                { Language.English, "Add extension (e.g.: .mp4 or mp4):" },
                { Language.Spanish, "Agregar extensión (ej: .mp4 o mp4):" },
                { Language.French, "Ajouter une extension (ex: .mp4 ou mp4):" },
                { Language.German, "Erweiterung hinzufügen (z.B.: .mp4 oder mp4):" },
                { Language.Italian, "Aggiungi estensione (es: .mp4 o mp4):" },
                { Language.Japanese, "拡張子を追加（例: .mp4 または mp4）:" }
            }},
            { "TOTAL_EXTENSIONS_COUNT", new Dictionary<Language, string> {
                { Language.English, "Total: {0}" },
                { Language.Spanish, "Total: {0}" },
                { Language.French, "Total: {0}" },
                { Language.German, "Gesamt: {0}" },
                { Language.Italian, "Totale: {0}" },
                { Language.Japanese, "合計: {0}" }
            }},
            { "ERROR_ENTER_EXTENSION", new Dictionary<Language, string> {
                { Language.English, "Please enter an extension." },
                { Language.Spanish, "Por favor ingresa una extensión." },
                { Language.French, "Veuillez entrer une extension." },
                { Language.German, "Bitte geben Sie eine Erweiterung ein." },
                { Language.Italian, "Per favore inserisci un'estensione." },
                { Language.Japanese, "拡張子を入力してください。" }
            }},
            { "ERROR_INVALID_EXTENSION", new Dictionary<Language, string> {
                { Language.English, "Invalid extension. Use format: .ext or ext" },
                { Language.Spanish, "La extensión no es válida. Use formato: .ext o ext" },
                { Language.French, "Extension invalide. Utilisez le format: .ext ou ext" },
                { Language.German, "Ungültige Erweiterung. Verwenden Sie das Format: .ext oder ext" },
                { Language.Italian, "Estensione non valida. Usa il formato: .ext o ext" },
                { Language.Japanese, "無効な拡張子です。形式を使用してください: .ext または ext" }
            }},
            { "ERROR_EXTENSION_ALREADY_BLOCKED", new Dictionary<Language, string> {
                { Language.English, "This extension is already in the blacklist." },
                { Language.Spanish, "Esta extensión ya está en la blacklist." },
                { Language.French, "Cette extension est déjà dans la liste noire." },
                { Language.German, "Diese Erweiterung ist bereits in der Blacklist." },
                { Language.Italian, "Questa estensione è già nella blacklist." },
                { Language.Japanese, "この拡張子はすでにブラックリストにあります。" }
            }},
            { "ERROR_SELECT_EXTENSIONS", new Dictionary<Language, string> {
                { Language.English, "Please select extensions to remove from blacklist." },
                { Language.Spanish, "Por favor marca las extensiones que deseas quitar del bloqueo." },
                { Language.French, "Veuillez sélectionner les extensions à retirer de la liste noire." },
                { Language.German, "Bitte wählen Sie die zu entfernenden Erweiterungen aus." },
                { Language.Italian, "Per favore seleziona le estensioni da rimuovere dalla blacklist." },
                { Language.Japanese, "ブラックリストから削除する拡張子を選択してください。" }
            }},
            { "CONFIRM_REMOVE_BLACKLIST", new Dictionary<Language, string> {
                { Language.English, "Are you sure you want to remove {0} extension(s) from the blacklist?\n\nThese extensions will start appearing in the analysis." },
                { Language.Spanish, "¿Estás seguro de quitar {0} extensión(es) de la blacklist?\n\nEstas extensiones comenzarán a aparecer en el análisis." },
                { Language.French, "Êtes-vous sûr de vouloir retirer {0} extension(s) de la liste noire?\n\nCes extensions commenceront à apparaître dans l'analyse." },
                { Language.German, "Möchten Sie wirklich {0} Erweiterung(en) aus der Blacklist entfernen?\n\nThese extensions will start appearing in the analysis." },
                { Language.Italian, "Sei sicuro di voler rimuovere {0} estensione/i dalla blacklist?\n\nQueste estensioni inizieranno ad apparire nell'analisi." },
                { Language.Japanese, "ブラックリストから {0} 個の拡張子を削除してもよろしいですか？\n\nこれらの拡張子は分析に表示され始めます。" }
            }},
            { "BTN_REMOVE_SELECTED", new Dictionary<Language, string> {
                { Language.English, "Remove Selected" },
                { Language.Spanish, "Eliminar Seleccionadas" },
                { Language.French, "Supprimer Sélectionnées" },
                { Language.German, "Ausgewählte Entfernen" },
                { Language.Italian, "Rimuovi Selezionate" },
                { Language.Japanese, "選択を削除" }
            }},
            { "MIN_SIZE_SUFFIX", new Dictionary<Language, string> {
                { Language.English, " - {0} minimum size allowed in analysis" },
                { Language.Spanish, " - {0} tamaño mínimo permitido en análisis" },
                { Language.French, " - {0} taille minimale autorisée dans l'analyse" },
                { Language.German, " - {0} Mindestgröße in Analyse erlaubt" },
                { Language.Italian, " - {0} dimensione minima consentita nell'analisi" },
                { Language.Japanese, " - {0} 分析で許可される最小サイズ" }
            }},
            { "CONFIG_MIN_SIZE_TITLE", new Dictionary<Language, string> {
                { Language.English, "Configure Minimum File Size" },
                { Language.Spanish, "Configurar Tamaño Mínimo de Archivo" },
                { Language.French, "Configurer la Taille Minimale du Fichier" },
                { Language.German, "Minimale Dateigröße Konfigurieren" },
                { Language.Italian, "Configura Dimensione Minima File" },
                { Language.Japanese, "最小ファイルサイズを設定" }
            }},
            { "MIN_SIZE_DESCRIPTION", new Dictionary<Language, string> {
                { Language.English, "Set the minimum file size for extension '{0}':" },
                { Language.Spanish, "Establece el tamaño mínimo de archivo para la extensión '{0}':" },
                { Language.French, "Définir la taille minimale du fichier pour l'extension '{0}':" },
                { Language.German, "Legen Sie die Mindestdateigröße für die Erweiterung '{0}' fest:" },
                { Language.Italian, "Imposta la dimensione minima del file per l'estensione '{0}':" },
                { Language.Japanese, "拡張子 '{0}' の最小ファイルサイズを設定:" }
            }},
            { "SIZE_IN_KB", new Dictionary<Language, string> {
                { Language.English, "Size in KB:" },
                { Language.Spanish, "Tamaño en KB:" },
                { Language.French, "Taille en KB:" },
                { Language.German, "Größe in KB:" },
                { Language.Italian, "Dimensione in KB:" },
                { Language.Japanese, "サイズ (KB):" }
            }},
            { "BTN_ACCEPT", new Dictionary<Language, string> {
                { Language.English, "Accept" },
                { Language.Spanish, "Aceptar" },
                { Language.French, "Accepter" },
                { Language.German, "Akzeptieren" },
                { Language.Italian, "Accetta" },
                { Language.Japanese, "承諾" }
            }},
            { "ERROR_NO_EXTENSION_SELECTED", new Dictionary<Language, string> {
                { Language.English, "Please select an extension from the list." },
                { Language.Spanish, "Por favor selecciona una extensión de la lista." },
                { Language.French, "Veuillez sélectionner une extension de la liste." },
                { Language.German, "Bitte wählen Sie eine Erweiterung aus der Liste." },
                { Language.Italian, "Seleziona un'estensione dalla lista." },
                { Language.Japanese, "リストから拡張子を選択してください。" }
            }},
            { "ERROR_SELECT_ONE_EXTENSION", new Dictionary<Language, string> {
                { Language.English, "Please select only ONE extension to configure." },
                { Language.Spanish, "Por favor selecciona SOLO UNA extensión para configurar." },
                { Language.French, "Veuillez sélectionner UNE SEULE extension à configurer." },
                { Language.German, "Bitte wählen Sie NUR EINE Erweiterung zum Konfigurieren." },
                { Language.Italian, "Seleziona SOLO UNA estensione da configurare." },
                { Language.Japanese, "設定する拡張子を1つだけ選択してください。" }
            }},
            { "INFO", new Dictionary<Language, string> {
                { Language.English, "Information" },
                { Language.Spanish, "Información" },
                { Language.French, "Information" },
                { Language.German, "Information" },
                { Language.Italian, "Informazione" },
                { Language.Japanese, "情報" }
            }},
            { "MIN_SIZE_CONFIG_DESCRIPTION_1", new Dictionary<Language, string> {
                { Language.English, "Configure the minimum size (in KB) for each extension." },
                { Language.Spanish, "Configura el tamaño mínimo (en KB) para cada extensión." },
                { Language.French, "Configurez la taille minimale (en KB) pour chaque extension." },
                { Language.German, "Konfigurieren Sie die Mindestgröße (in KB) für jede Erweiterung." },
                { Language.Italian, "Configura la dimensione minima (in KB) per ogni estensione." },
                { Language.Japanese, "各拡張子の最小サイズ（KB単位）を設定します。" }
            }},
            { "MIN_SIZE_CONFIG_DESCRIPTION_2", new Dictionary<Language, string> {
                { Language.English, "Smaller files will be ignored during analysis." },
                { Language.Spanish, "Archivos más pequeños serán ignorados durante el análisis." },
                { Language.French, "Les fichiers plus petits seront ignorés lors de l'analyse." },
                { Language.German, "Kleinere Dateien werden bei der Analyse ignoriert." },
                { Language.Italian, "I file più piccoli verranno ignorati durante l'analisi." },
                { Language.Japanese, "小さいファイルは分析中に無視されます。" }
            }},
            { "BTN_APPLY_TO_ALL", new Dictionary<Language, string> {
                { Language.English, "Apply to All" },
                { Language.Spanish, "Aplicar a Todas" },
                { Language.French, "Appliquer à Tous" },
                { Language.German, "Auf Alle Anwenden" },
                { Language.Italian, "Applica a Tutti" },
                { Language.Japanese, "すべてに適用" }
            }},
            { "EXTENSION_COLUMN", new Dictionary<Language, string> {
                { Language.English, "Extension" },
                { Language.Spanish, "Extensión" },
                { Language.French, "Extension" },
                { Language.German, "Erweiterung" },
                { Language.Italian, "Estensione" },
                { Language.Japanese, "拡張子" }
            }},
            { "SIZE_KB_COLUMN", new Dictionary<Language, string> {
                { Language.English, "Size (KB)" },
                { Language.Spanish, "Tamaño (KB)" },
                { Language.French, "Taille (KB)" },
                { Language.German, "Größe (KB)" },
                { Language.Italian, "Dimensione (KB)" },
                { Language.Japanese, "サイズ (KB)" }
            }},
            { "PRESETS", new Dictionary<Language, string> {
                { Language.English, "Presets" },
                { Language.Spanish, "Presets" },
                { Language.French, "Préréglages" },
                { Language.German, "Voreinstellungen" },
                { Language.Italian, "Preimpostazioni" },
                { Language.Japanese, "プリセット" }
            }},
            { "NO_LIMIT", new Dictionary<Language, string> {
                { Language.English, "No limit (0 KB)" },
                { Language.Spanish, "Sin límite (0 KB)" },
                { Language.French, "Sans limite (0 KB)" },
                { Language.German, "Kein Limit (0 KB)" },
                { Language.Italian, "Nessun limite (0 KB)" },
                { Language.Japanese, "制限なし (0 KB)" }
            }},
            { "KB_UNIT", new Dictionary<Language, string> {
                { Language.English, "{0} KB" },
                { Language.Spanish, "{0} KB" },
                { Language.French, "{0} KB" },
                { Language.German, "{0} KB" },
                { Language.Italian, "{0} KB" },
                { Language.Japanese, "{0} KB" }
            }},
            { "MB_UNIT", new Dictionary<Language, string> {
                { Language.English, "{0} MB ({1} KB)" },
                { Language.Spanish, "{0} MB ({1} KB)" },
                { Language.French, "{0} MB ({1} KB)" },
                { Language.German, "{0} MB ({1} KB)" },
                { Language.Italian, "{0} MB ({1} KB)" },
                { Language.Japanese, "{0} MB ({1} KB)" }
            }},
            { "ALL_EXTENSIONS", new Dictionary<Language, string> {
                { Language.English, "(All extensions)" },
                { Language.Spanish, "(Todas las extensiones)" },
                { Language.French, "(Toutes les extensions)" },
                { Language.German, "(Alle Erweiterungen)" },
                { Language.Italian, "(Tutte le estensioni)" },
                { Language.Japanese, "(すべての拡張子)" }
            }},
            { "SORT_BY", new Dictionary<Language, string> {
                { Language.English, "Sort by" },
                { Language.Spanish, "Ordenar por" },
                { Language.French, "Trier par" },
                { Language.German, "Sortieren nach" },
                { Language.Italian, "Ordina per" },
                { Language.Japanese, "並べ替え" }
            }},
            { "SORT_NAME_AZ", new Dictionary<Language, string> {
                { Language.English, "Name (A-Z)" },
                { Language.Spanish, "Nombre (A-Z)" },
                { Language.French, "Nom (A-Z)" },
                { Language.German, "Name (A-Z)" },
                { Language.Italian, "Nome (A-Z)" },
                { Language.Japanese, "名前 (A-Z)" }
            }},
            { "SORT_SIZE_DESC", new Dictionary<Language, string> {
                { Language.English, "Size (Largest to Smallest)" },
                { Language.Spanish, "Tamaño (Mayor a Menor)" },
                { Language.French, "Taille (Plus Grand au Plus Petit)" },
                { Language.German, "Größe (Größte zu Kleinste)" },
                { Language.Italian, "Dimensione (Maggiore a Minore)" },
                { Language.Japanese, "サイズ (大きい順)" }
            }},
            { "SORT_SIZE_ASC", new Dictionary<Language, string> {
                { Language.English, "Size (Smallest to Largest)" },
                { Language.Spanish, "Tamaño (Menor a Mayor)" },
                { Language.French, "Taille (Plus Petit au Plus Grand)" },
                { Language.German, "Größe (Kleinste zu Größte)" },
                { Language.Italian, "Dimensione (Minore a Maggiore)" },
                { Language.Japanese, "サイズ (小さい順)" }
            }},
            { "SORT_EXTENSION", new Dictionary<Language, string> {
                { Language.English, "Extension" },
                { Language.Spanish, "Extensión" },
                { Language.French, "Extension" },
                { Language.German, "Erweiterung" },
                { Language.Italian, "Estensione" },
                { Language.Japanese, "拡張子" }
            }},
            { "SORT_DATE_MODIFIED_DESC", new Dictionary<Language, string> {
                { Language.English, "Modified Date (Recent)" },
                { Language.Spanish, "Fecha Modificación (Reciente)" },
                { Language.French, "Date de Modification (Récent)" },
                { Language.German, "Änderungsdatum (Neueste)" },
                { Language.Italian, "Data Modifica (Recente)" },
                { Language.Japanese, "変更日 (新しい順)" }
            }},
            { "SORT_DATE_MODIFIED_ASC", new Dictionary<Language, string> {
                { Language.English, "Modified Date (Oldest)" },
                { Language.Spanish, "Fecha Modificación (Antigua)" },
                { Language.French, "Date de Modification (Ancien)" },
                { Language.German, "Änderungsdatum (Älteste)" },
                { Language.Italian, "Data Modifica (Vecchia)" },
                { Language.Japanese, "変更日 (古い順)" }
            }},
            { "SORT_DATE_CREATED_DESC", new Dictionary<Language, string> {
                { Language.English, "Creation Date (Recent)" },
                { Language.Spanish, "Fecha Creación (Reciente)" },
                { Language.French, "Date de Création (Récent)" },
                { Language.German, "Erstellungsdatum (Neueste)" },
                { Language.Italian, "Data Creazione (Recente)" },
                { Language.Japanese, "作成日 (新しい順)" }
            }},
            { "SORT_DATE_CREATED_ASC", new Dictionary<Language, string> {
                { Language.English, "Creation Date (Oldest)" },
                { Language.Spanish, "Fecha Creación (Antigua)" },
                { Language.French, "Date de Création (Ancien)" },
                { Language.German, "Erstellungsdatum (Älteste)" },
                { Language.Italian, "Data Creazione (Vecchia)" },
                { Language.Japanese, "作成日 (古い順)" }
            }},
            { "SORT_FOLDER_AZ", new Dictionary<Language, string> {
                { Language.English, "Folder (A-Z)" },
                { Language.Spanish, "Carpeta (A-Z)" },
                { Language.French, "Dossier (A-Z)" },
                { Language.German, "Ordner (A-Z)" },
                { Language.Italian, "Cartella (A-Z)" },
                { Language.Japanese, "フォルダー (A-Z)" }
            }},
            { "TIP_DOUBLE_CLICK", new Dictionary<Language, string> {
                { Language.English, "💡Tip: Double-click on a file to open its location" },
                { Language.Spanish, "💡Tip: Haz doble clic en un archivo para abrir su ubicación" },
                { Language.French, "💡Astuce: Double-cliquez sur un fichier pour ouvrir son emplacement" },
                { Language.German, "💡Tipp: Doppelklicken Sie auf eine Datei, um ihren Speicherort zu öffnen" },
                { Language.Italian, "💡Suggerimento: Fai doppio clic su un file per aprire la sua posizione" },
                { Language.Japanese, "💡ヒント: ファイルをダブルクリックして場所を開く" }
            }},
            { "BLACKLIST_IGNORE", new Dictionary<Language, string> {
                { Language.English, "BLACKLIST (Ignore in analysis)" },
                { Language.Spanish, "BLACKLIST (Ignorar en análisis)" },
                { Language.French, "LISTE NOIRE (Ignorer dans l'analyse)" },
                { Language.German, "BLACKLIST (In Analyse ignorieren)" },
                { Language.Italian, "BLACKLIST (Ignora nell'analisi)" },
                { Language.Japanese, "ブラックリスト (分析で無視)" }
            }},
            { "TIP_MOVE_EXTENSIONS", new Dictionary<Language, string> {
                { Language.English, "💡Check extensions → Select destination → Press MOVE" },
                { Language.Spanish, "💡Marca extensiones → Selecciona destino → Presiona MOVER" },
                { Language.French, "💡Cochez les extensions → Sélectionnez la destination → Appuyez sur DÉPLACER" },
                { Language.German, "💡Markieren Sie Erweiterungen → Wählen Sie Ziel → Drücken Sie VERSCHIEBEN" },
                { Language.Italian, "💡Spunta estensioni → Seleziona destinazione → Premi SPOSTA" },
                { Language.Japanese, "💡拡張子をチェック → 移動先を選択 → 移動を押す" }
            }},
            { "STATUS_ANALYZING", new Dictionary<Language, string> {
                { Language.English, "Scanning files (Filter: 15KB)..." },
                { Language.Spanish, "Escaneando archivos (Filtro: 15KB)..." },
                { Language.French, "Analyse des fichiers (Filtre: 15KB)..." },
                { Language.German, "Dateien scannen (Filter: 15KB)..." },
                { Language.Italian, "Scansione file (Filtro: 15KB)..." },
                { Language.Japanese, "ファイルをスキャン中 (フィルター: 15KB)..." }
            }},
            { "STATUS_ANALYSIS_COMPLETE", new Dictionary<Language, string> {
                { Language.English, "Analysis complete. Found {0} valid files." },
                { Language.Spanish, "Análisis terminado. Se encontraron {0} archivos válidos." },
                { Language.French, "Analyse terminée. {0} fichiers valides trouvés." },
                { Language.German, "Analyse abgeschlossen. {0} gültige Dateien gefunden." },
                { Language.Italian, "Analisi completata. Trovati {0} file validi." },
                { Language.Japanese, "分析完了。{0} 個の有効なファイルが見つかりました。" }
            }},
            { "STATUS_RESET", new Dictionary<Language, string> {
                { Language.English, "All cleared." },
                { Language.Spanish, "Todo limpio." },
                { Language.French, "Tout effacé." },
                { Language.German, "Alles gelöscht." },
                { Language.Italian, "Tutto pulito." },
                { Language.Japanese, "すべてクリアされました。" }
            }},
            { "STATUS_RESCUE_COMPLETE", new Dictionary<Language, string> {
                { Language.English, "Rescue complete!" },
                { Language.Spanish, "¡Rescate finalizado!" },
                { Language.French, "Sauvetage terminé!" },
                { Language.German, "Rettung abgeschlossen!" },
                { Language.Italian, "Salvataggio completato!" },
                { Language.Japanese, "救出完了！" }
            }},
            { "NO_CATEGORIES_SELECTED", new Dictionary<Language, string> {
                { Language.English, "No categories selected." },
                { Language.Spanish, "No hay categorías seleccionadas." },
                { Language.French, "Aucune catégorie sélectionnée." },
                { Language.German, "Keine Kategorien ausgewählt." },
                { Language.Italian, "Nessuna categoria selezionata." },
                { Language.Japanese, "カテゴリーが選択されていません。" }
            }},
            { "HELP_CONTENT", new Dictionary<Language, string> {
                { Language.English, "HOW TO USE KILOFILTER\n\nQUICK START GUIDE\n\nStep 1: Select Source Folder\n• Click \"Browse...\" next to \"SOURCE FOLDER\"\n• Navigate to the folder containing the files you want to organize\n• This can be your Downloads folder, an external drive, or any directory with mixed files\n\nStep 2: Analyze Files\n• Click \"1. ANALYZE DISK\" to start scanning\n• The program will scan all files and automatically categorize them by type\n• Wait until you see \"Analysis complete\" at the bottom\n\nStep 3: Review Results\n• Check the list of categories (Images, Videos, Documents, etc.)\n• Each row shows: Category name, Number of files, Total size\n• Click \"View Details\" on any category to see individual files\n\nStep 4: Configure (Optional)\n• Click \"⚙️ Configure\" to customize file extensions for each category\n• Use the \"BLACKLIST\" tab to exclude unwanted file types from analysis\n• Set minimum file sizes to ignore small temporary files\n\nStep 5: Create Custom Categories (Optional)\n• Click \"➕ New Category\" to create your own file groups\n• Enter a category name (e.g., \"ProjectFiles\", \"Photos2024\")\n• Add file extensions (.psd, .ai, .indd, etc.)\n• Choose to analyze immediately or save for later\n\nStep 6: Select Destination\n• Click \"Browse...\" next to \"DESTINATION FOLDER\"\n• Choose where you want to save the organized files\n• A new folder will be created automatically with date/time stamp\n\nStep 7: Rescue Files\n• Check/uncheck categories you want to copy\n• Click \"2. RESCUE SELECTED\"\n• Files will be copied (not moved) to destination in organized folders\n• Original files remain untouched in the source location\n\nADVANCED FEATURES\n• Double-click any file in the detail view to open its location\n• Use the language button (top-right) to switch between 6 languages\n• The \"CLEAR\" button resets everything if you want to start over\n• Analysis doesn't modify your original files - they're only copied when you \"Rescue\"\n• Set per-extension minimum file sizes\n• Use blacklist to exclude specific file types from analysis" },
                { Language.Spanish, "CÓMO USAR KILOFILTER\n\nGUÍA DE INICIO RÁPIDO\n\nPaso 1: Seleccionar Carpeta de Origen\n• Haz clic en \"Examinar...\" junto a \"CARPETA DE ORIGEN\"\n• Navega hasta la carpeta que contiene los archivos que deseas organizar\n• Puede ser tu carpeta de Descargas, un disco externo o cualquier directorio con archivos mezclados\n\nPaso 2: Analizar Archivos\n• Haz clic en \"1. ANALIZAR DISCO\" para iniciar el escaneo\n• El programa escaneará todos los archivos y los categorizará automáticamente por tipo\n• Espera hasta ver \"Análisis terminado\" en la parte inferior\n\nPaso 3: Revisar Resultados\n• Revisa la lista de categorías (Imágenes, Videos, Documentos, etc.)\n• Cada fila muestra: Nombre de categoría, Cantidad de archivos, Tamaño total\n• Haz clic en \"Ver Detalle\" en cualquier categoría para ver archivos individuales\n\nPaso 4: Configurar (Opcional)\n• Haz clic en \"⚙️ Configurar\" para personalizar extensiones de archivo por categoría\n• Usa la pestaña \"BLACKLIST\" para excluir tipos de archivo no deseados del análisis\n• Establece tamaños mínimos de archivo para ignorar archivos temporales pequeños\n\nPaso 5: Crear Categorías Personalizadas (Opcional)\n• Haz clic en \"➕ Nueva Categoría\" para crear tus propios grupos de archivos\n• Ingresa un nombre de categoría (ej: \"ArchivosProyecto\", \"Fotos2024\")\n• Agrega extensiones de archivo (.psd, .ai, .indd, etc.)\n• Elige analizar inmediatamente o guardar para después\n\nPaso 6: Seleccionar Destino\n• Haz clic en \"Examinar...\" junto a \"CARPETA DE DESTINO\"\n• Elige dónde quieres guardar los archivos organizados\n• Se creará automáticamente una nueva carpeta con fecha/hora\n\nPaso 7: Rescatar Archivos\n• Marca/desmarca las categorías que deseas copiar\n• Haz clic en \"2. RESCATAR SELECCIONADOS\"\n• Los archivos se copiarán (no se moverán) al destino en carpetas organizadas\n• Los archivos originales permanecen intactos en la ubicación de origen\n\nFUNCIONES AVANZADAS\n• Haz doble clic en cualquier archivo en la vista detallada para abrir su ubicación\n• Usa el botón de idioma (arriba a la derecha) para cambiar entre 6 idiomas\n• El botón \"LIMPIAR\" reinicia todo si quieres empezar de nuevo\n• El análisis no modifica tus archivos originales - solo se copian cuando haces \"Rescatar\"\n• Establece tamaños mínimos de archivo por extensión\n• Usa blacklist para excluir tipos de archivo específicos del análisis" },
                { Language.French, "COMMENT UTILISER KILOFILTER\n\nGUIDDÉ DE DÉMARRAGE RAPIDE\n\nÉtape 1 : Sélectionner le Dossier Source\n• Cliquez sur \"Parcourir...\" à côté de \"DOSSIER SOURCE\"\n• Naviguez jusqu'au dossier contenant les fichiers que vous souhaitez organiser\n• Cela peut être votre dossier Téléchargements, un disque externe ou tout répertoire avec des fichiers mélangés\n\nÉtape 2 : Analyser les Fichiers\n• Cliquez sur \"1. ANALYSER LE DISQUE\" pour démarrer l'analyse\n• Le programme analysera tous les fichiers et les catégorisera automatiquement par type\n• Attendez de voir \"Analyse terminée\" en bas\n\nÉtape 3 : Examiner les Résultats\n• Vérifiez la liste des catégories (Images, Vidéos, Documents, etc.)\n• Chaque ligne affiche : Nom de catégorie, Nombre de fichiers, Taille totale\n• Cliquez sur \"Voir Détails\" sur n'importe quelle catégorie pour voir les fichiers individuels\n\nÉtape 4 : Configurer (Optionnel)\n• Cliquez sur \"⚙️ Configurer\" pour personnaliser les extensions de fichier par catégorie\n• Utilisez l'onglet \"LISTE NOIRE\" pour exclure les types de fichiers indésirables de l'analyse\n• Définissez des tailles de fichier minimales pour ignorer les petits fichiers temporaires\n\nÉtape 5 : Créer des Catégories Personnalisées (Optionnel)\n• Cliquez sur \"➕ Nouvelle Catégorie\" pour créer vos propres groupes de fichiers\n• Entrez un nom de catégorie (ex: \"FichiersProjet\", \"Photos2024\")\n• Ajoutez des extensions de fichier (.psd, .ai, .indd, etc.)\n• Choisissez d'analyser immédiatement ou de sauvegarder pour plus tard\n\nÉtape 6 : Sélectionner la Destination\n• Cliquez sur \"Parcourir...\" à côté de \"DOSSIER DE DESTINATION\"\n• Choisissez où vous voulez enregistrer les fichiers organisés\n• Un nouveau dossier sera créé automatiquement avec date/heure\n\nÉtape 7 : Sauvegarder les Fichiers\n• Cochez/décochez les catégories que vous souhaitez copier\n• Cliquez sur \"2. SAUVEGARDER SÉLECTIONNÉS\"\n• Les fichiers seront copiés (pas déplacés) vers la destination dans des dossiers organisés\n• Les fichiers originaux restent intacts à l'emplacement source\n\nFONCTIONNALITÉS AVANCÉES\n• Double-cliquez sur n'importe quel fichier dans la vue détaillée pour ouvrir son emplacement\n• Utilisez le bouton de langue (en haut à droite) pour changer de langue\n• Le bouton \"EFFACER\" réinitialise tout si vous voulez recommencer\n• L'analyse ne modifie pas vos fichiers originaux - ils ne sont copiés que lorsque vous \"Sauvegardez\"\n• Définir des tailles minimales de fichier par extension\n• Utilisez la liste noire pour exclure des types de fichiers spécifiques de l'analyse" },
                { Language.German, "SO VERWENDEN SIE KILOFILTER\n\nSCHNELLSTARTANLEITUNG\n\nSchritt 1: Quellordner Auswählen\n• Klicken Sie auf \"Durchsuchen...\" neben \"QUELLORDNER\"\n• Navigieren Sie zu dem Ordner mit den Dateien, die Sie organisieren möchten\n• Dies kann Ihr Downloads-Ordner, eine externe Festplatte oder ein beliebiges Verzeichnis mit gemischten Dateien sein\n\nSchritt 2: Dateien Analysieren\n• Klicken Sie auf \"1. FESTPLATTE ANALYSIEREN\", um den Scan zu starten\n• Das Programm scannt alle Dateien und kategorisiert sie automatisch nach Typ\n• Warten Sie, bis unten \"Analyse abgeschlossen\" angezeigt wird\n\nSchritt 3: Ergebnisse Überprüfen\n• Überprüfen Sie die Liste der Kategorien (Bilder, Videos, Dokumente usw.)\n• Jede Zeile zeigt: Kategoriename, Anzahl der Dateien, Gesamtgröße\n• Klicken Sie auf \"Details Anzeigen\" bei jeder Kategorie, um einzelne Dateien zu sehen\n\nSchritt 4: Konfigurieren (Optional)\n• Klicken Sie auf \"⚙️ Konfigurieren\", um Dateierweiterungen pro Kategorie anzupassen\n• Verwenden Sie die Registerkarte \"BLACKLIST\", um unerwünschte Dateitypen von der Analyse auszuschließen\n• Legen Sie Mindestdateigrößen fest, um kleine temporäre Dateien zu ignorieren\n\nSchritt 5: Benutzerdefinierte Kategorien Erstellen (Optional)\n• Klicken Sie auf \"➕ Neue Kategorie\", um eigene Dateigruppen zu erstellen\n• Geben Sie einen Kategorienamen ein (z.B. \"Projektdateien\", \"Fotos2024\")\n• Fügen Sie Dateierweiterungen hinzu (.psd, .ai, .indd usw.)\n• Wählen Sie, ob Sie sofort analysieren oder für später speichern möchten\n\nSchritt 6: Ziel Auswählen\n• Klicken Sie auf \"Durchsuchen...\" neben \"ZIELORDNER\"\n• Wählen Sie, wo Sie die organisierten Dateien speichern möchten\n• Ein neuer Ordner wird automatisch mit Datum/Uhrzeit erstellt\n\nSchritt 7: Dateien Retten\n• Aktivieren/Deaktivieren Sie die Kategorien, die Sie kopieren möchten\n• Klicken Sie auf \"2. AUSGEWÄHLTE RETTEN\"\n• Dateien werden in organisierte Ordner am Zielort kopiert (nicht verschoben)\n• Originaldateien bleiben am Quellort unverändert\n\nERWEITERTE FUNKTIONEN\n• Doppelklicken Sie auf eine Datei in der Detailansicht, um ihren Speicherort zu öffnen\n• Verwenden Sie die Sprachschaltfläche (oben rechts), um zwischen Sprachen zu wechseln\n• Die Schaltfläche \"LÖSCHEN\" setzt alles zurück, wenn Sie neu beginnen möchten\n• Die Analyse ändert Ihre Originaldateien nicht - sie werden nur beim \"Retten\" kopiert\n• Legen Sie Mindestdateigrößen pro Erweiterung fest\n• Verwenden Sie die Blacklist, um bestimmte Dateitypen von der Analyse auszuschließen" },
                { Language.Italian, "COME USARE KILOFILTER\n\nGUIDEA RAPIDA\n\nPassaggio 1: Selezionare la Cartella Sorgente\n• Clicca su \"Sfoglia...\" accanto a \"CARTELLA SORGENTE\"\n• Naviga fino alla cartella contenente i file che vuoi organizzare\n• Può essere la tua cartella Download, un disco esterno o qualsiasi directory con file misti\n\nPassaggio 2: Analizzare i File\n• Clicca su \"1. ANALIZZA DISCO\" per avviare la scansione\n• Il programma scansionerà tutti i file e li categorizzerà automaticamente per tipo\n• Attendi fino a vedere \"Analisi completata\" in basso\n\nPassaggio 3: Rivedere i Risultati\n• Controlla l'elenco delle categorie (Immagini, Video, Documenti, ecc.)\n• Ogni riga mostra: Nome categoria, Numero di file, Dimensione totale\n• Clicca su \"Visualizza Dettagli\" su qualsiasi categoria per vedere i singoli file\n\nPassaggio 4: Configurare (Opzionale)\n• Clicca su \"⚙️ Configura\" per personalizzare le estensioni dei file per categoria\n• Usa la scheda \"BLACKLIST\" per escludere tipi di file indesiderati dall'analisi\n• Imposta dimensioni minime dei file per ignorare piccoli file temporanei\n\nPassaggio 5: Creare Categorie Personalizzate (Opzionale)\n• Clicca su \"➕ Nuova Categoria\" per creare i tuoi gruppi di file\n• Inserisci un nome di categoria (es: \"FileProgetto\", \"Foto2024\")\n• Aggiungi estensioni di file (.psd, .ai, .indd, ecc.)\n• Scegli di analizzare immediatamente o salvare per dopo\n\nPassaggio 6: Selezionare la Destinazione\n• Clicca su \"Sfoglia...\" accanto a \"CARTELLA DI DESTINAZIONE\"\n• Scegli dove vuoi salvare i file organizzati\n• Una nuova cartella verrà creata automaticamente con data/ora\n\nPassaggio 7: Salvare i File\n• Seleziona/deseleziona le categorie che vuoi copiare\n• Clicca su \"2. SALVA SELEZIONATI\"\n• I file verranno copiati (non spostati) nella destinazione in cartelle organizzate\n• I file originali rimangono intatti nella posizione sorgente\n\nFUNZIONALITÀ AVANZATE\n• Fai doppio clic su qualsiasi file nella vista dettagliata per aprire la sua posizione\n• Usa il pulsante lingua (in alto a destra) per cambiare lingua\n• Il pulsante \"PULISCI\" resetta tutto se vuoi ricominciare\n• L'analisi non modifica i tuoi file originali - vengono copiati solo quando \"Salvi\"\n• Imposta dimensioni minime diverse per file per estensione\n• Usa la blacklist per escludere tipi di file specifici dall'analisi" },
                { Language.Japanese, "KILOFILTERの使い方\n\nクイックスタートガイド\n\nステップ1：ソースフォルダーを選択\n• 「ソースフォルダー」の横にある「参照...」をクリック\n• 整理したいファイルが含まれているフォルダーに移動\n• ダウンロードフォルダー、外部ドライブ、または混在ファイルのあるディレクトリを選択可能\n\nステップ2：ファイルを分析\n• 「1. ディスクを分析」をクリックしてスキャンを開始\n• プログラムがすべてのファイルをスキャンし、タイプ別に自動分類\n• 下部に「分析完了」と表示されるまで待つ\n\nステップ3：結果を確認\n• カテゴリーのリスト（画像、動画、ドキュメントなど）を確認\n• 各行に表示：カテゴリー名、ファイル数、合計サイズ\n• 任意のカテゴリーの「詳細を表示」をクリックして個別ファイルを表示\n\nステップ4：設定（オプション）\n• 「⚙️ 設定」をクリックして、カテゴリーごとのファイル拡張子をカスタマイズ\n• 「ブラックリスト」タブを使用して、不要なファイルタイプを分析から除外\n• 小さな一時ファイルを無視するために最小ファイルサイズを設定\n\nステップ5：カスタムカテゴリーを作成（オプション）\n• 「➕ 新しいカテゴリー」をクリックして独自のファイルグループを作成\n• カテゴリー名を入力（例：「プロジェクトファイル」、「写真2024」）\n• ファイル拡張子を追加（.psd、.ai、.inddなど）\n• すぐに分析するか、後で保存するかを選択\n\nステップ6：保存先を選択\n• 「保存先フォルダー」の横にある「参照...」をクリック\n• 整理されたファイルを保存する場所を選択\n• 日付/時刻付きの新しいフォルダーが自動的に作成されます\n\nステップ7：ファイルを救出\n• コピーしたいカテゴリーをチェック/チェック解除\n• 「2. 選択を救出」をクリック\n• ファイルは整理されたフォルダーに保存先へコピー（移動ではない）\n• 元のファイルはソースの場所にそのまま残る\n\n高度な機能\n• ファイルをダブルクリックして、その場所を開く\n• 言語ボタン（右上）を使用して言語を切り替える\n• 「クリア」ボタンは、やり直したい場合にすべてをリセット\n• 分析は元のファイルを変更しません - 「救出」時にのみコピーされます\n• 拡張子ごとに異なる最小ファイルサイズを設定\n• ブラックリストを使用して、特定のファイルタイプを分析から除外" }
            }},
            { "BTN_HELP", new Dictionary<Language, string> {
                { Language.English, "❓ Help" },
                { Language.Spanish, "❓ Ayuda" },
                { Language.French, "❓ Aide" },
                { Language.German, "❓ Hilfe" },
                { Language.Italian, "❓ Aiuto" },
                { Language.Japanese, "❓ ヘルプ" }
            }},
            { "HELP_TITLE", new Dictionary<Language, string> {
                { Language.English, "KiloFilter Help - How to Use" },
                { Language.Spanish, "Ayuda de KiloFilter - Cómo Usar" },
                { Language.French, "Aide KiloFilter - Comment Utiliser" },
                { Language.German, "KiloFilter Hilfe - Anleitung" },
                { Language.Italian, "Aiuto KiloFilter - Come Usare" },
                { Language.Japanese, "KiloFilter ヘルプ - 使い方" }
            }}
        };

        public static string Get(string key)
        {
            if (translations.ContainsKey(key) && translations[key].ContainsKey(CurrentLanguage))
            {
                return translations[key][CurrentLanguage];
            }
            return key;
        }

        public static string GetFolderName(string internalKey)
        {
            // Mapeo de claves internas a claves de traducción
            var folderMapping = new Dictionary<string, string>
            {
                { "Imagenes", "CAT_IMAGES" },
                { "Videos", "CAT_VIDEOS" },
                { "Documentos", "CAT_DOCUMENTS" },
                { "Audio", "CAT_AUDIO" },
                { "Comprimidos", "CAT_COMPRESSED" },
                { "JuegosYMundos", "CAT_GAMES" },
                { "AplicacionesAPK", "CAT_APPS" },
                { "BasesDeDatos", "CAT_DATABASES" },
                { "CodigoFuente", "CAT_SOURCE_CODE" },
                { "Modelos3D", "CAT_3D_MODELS" },
                { "Ebooks", "CAT_EBOOKS" },
                { "Subtitulos", "CAT_SUBTITLES" },
                { "LoDemas", "CAT_OTHERS" }
            };
            
            if (folderMapping.ContainsKey(internalKey))
            {
                return Get(folderMapping[internalKey]);
            }
            return internalKey;
        }
    }
}
