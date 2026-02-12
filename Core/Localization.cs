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
                { Language.English, "☚ Browse..." },
                { Language.Spanish, "☚ Examinar..." },
                { Language.French, "☚ Parcourir..." },
                { Language.German, "☚ Durchsuchen..." },
                { Language.Italian, "☚ Sfoglia..." },
                { Language.Japanese, "☚ 参照..." }
            }},
            { "BTN_CLEAR", new Dictionary<Language, string> {
                { Language.English, "☓ CLEAR" },
                { Language.Spanish, "☓ LIMPIAR" },
                { Language.French, "☓ EFFACER" },
                { Language.German, "☓ LÖSCHEN" },
                { Language.Italian, "☓ PULISCI" },
                { Language.Japanese, "☓ クリア" }
            }},
            { "BTN_ANALYZE", new Dictionary<Language, string> {
                { Language.English, "⛏ ANALYZE DISK" },
                { Language.Spanish, "⛏ ANALIZAR DISCO" },
                { Language.French, "⛏ ANALYSER LE DISQUE" },
                { Language.German, "⛏ FESTPLATTE ANALYSIEREN" },
                { Language.Italian, "⛏ ANALIZZA DISCO" },
                { Language.Japanese, "⛏ ディスクを分析" }
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
                { Language.English, "⛑ RESCUE SELECTED" },
                { Language.Spanish, "⛑ RESCATAR SELECCIONADOS" },
                { Language.French, "⛑ SAUVEGARDER SÉLECTIONNÉS" },
                { Language.German, "⛑ AUSGEWÄHLTE RETTEN" },
                { Language.Italian, "⛑ SALVA SELEZIONATI" },
                { Language.Japanese, "⛑ 選択したものを救出" }
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
                { Language.English, "❌ Remove" },
                { Language.Spanish, "❌ Eliminar" },
                { Language.French, "❌ Supprimer" },
                { Language.German, "❌ Entfernen" },
                { Language.Italian, "❌ Rimuovi" },
                { Language.Japanese, "❌ 削除" }
            }},
            { "BTN_CLEAR_ALL", new Dictionary<Language, string> {
                { Language.English, "☒ Clear All" },
                { Language.Spanish, "☒ Limpiar Todo" },
                { Language.French, "☒ Tout Effacer" },
                { Language.German, "☒ Alles Löschen" },
                { Language.Italian, "☒ Pulisci Tutto" },
                { Language.Japanese, "☒ すべてクリア" }
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
                { Language.English, "➕ Add" },
                { Language.Spanish, "➕ Agregar" },
                { Language.French, "➕ Ajouter" },
                { Language.German, "➕ Hinzufügen" },
                { Language.Italian, "➕ Aggiungi" },
                { Language.Japanese, "➕ 追加" }
            }},
            { "BTN_SAVE_AND_ANALYZE", new Dictionary<Language, string> {
                { Language.English, "☑ Save and Analyze" },
                { Language.Spanish, "☑ Guardar y Analizar" },
                { Language.French, "☑ Enregistrer et Analyser" },
                { Language.German, "☑ Speichern und Analysieren" },
                { Language.Italian, "☑ Salva e Analizza" },
                { Language.Japanese, "☑ 保存して分析" }
            }},
            { "BTN_SAVE_ONLY", new Dictionary<Language, string> {
                { Language.English, "☑ Save Only" },
                { Language.Spanish, "☑ Solo Guardar" },
                { Language.French, "☑ Enregistrer Seulement" },
                { Language.German, "☑ Nur Speichern" },
                { Language.Italian, "☑ Solo Salva" },
                { Language.Japanese, "☑ 保存のみ" }
            }},
            { "BTN_CANCEL", new Dictionary<Language, string> {
                { Language.English, "❌ Cancel" },
                { Language.Spanish, "❌ Cancelar" },
                { Language.French, "❌ Annuler" },
                { Language.German, "❌ Abbrechen" },
                { Language.Italian, "❌ Annulla" },
                { Language.Japanese, "❌ キャンセル" }
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
                { Language.English, "✓ Apply Changes" },
                { Language.Spanish, "✓ Aplicar Cambios" },
                { Language.French, "✓ Appliquer les Modifications" },
                { Language.German, "✓ Änderungen Übernehmen" },
                { Language.Italian, "✓ Applica Modifiche" },
                { Language.Japanese, "✓ 変更を適用" }
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
                { Language.English, "🔍 Filter" },
                { Language.Spanish, "🔍 Filtrar" },
                { Language.French, "🔍 Filtrer" },
                { Language.German, "🔍 Filtern" },
                { Language.Italian, "🔍 Filtra" },
                { Language.Japanese, "🔍 フィルター" }
            }},
            { "BTN_COPY_SUMMARY", new Dictionary<Language, string> {
                { Language.English, "⧉ Copy Summary" },
                { Language.Spanish, "⧉ Copiar Resumen" },
                { Language.French, "⧉ Copier le Résumé" },
                { Language.German, "⧉ Zusammenfassung Kopieren" },
                { Language.Italian, "⧉ Copia Riepilogo" },
                { Language.Japanese, "⧉ サマリーをコピー" }
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
                { Language.English, "📂 MOVE" },
                { Language.Spanish, "📂 MOVER" },
                { Language.French, "📂 DÉPLACER" },
                { Language.German, "📂 VERSCHIEBEN" },
                { Language.Italian, "📂 SPOSTA" },
                { Language.Japanese, "📂 移動" }
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
                { Language.English, "➕ Remove from Blacklist" },
                { Language.Spanish, "➕ Quitar de Blacklist" },
                { Language.French, "➕ Retirer de la Liste Noire" },
                { Language.German, "➕ Von Blacklist Entfernen" },
                { Language.Italian, "➕ Rimuovi dalla Blacklist" },
                { Language.Japanese, "➕ ブラックリストから削除" }
            }},
            { "BTN_BLOCK", new Dictionary<Language, string> {
                { Language.English, "⛔ Block" },
                { Language.Spanish, "⛔ Bloquear" },
                { Language.French, "⛔ Bloquer" },
                { Language.German, "⛔ Blockieren" },
                { Language.Italian, "⛔ Blocca" },
                { Language.Japanese, "⛔ ブロック" }
            }},
            { "BTN_CHECK_ALL", new Dictionary<Language, string> {
                { Language.English, "✓ Check All" },
                { Language.Spanish, "✓ Marcar Todas" },
                { Language.French, "✓ Tout Cocher" },
                { Language.German, "✓ Alle Markieren" },
                { Language.Italian, "✓ Seleziona Tutto" },
                { Language.Japanese, "✓ すべて選択" }
            }},
            { "BTN_UNCHECK_ALL", new Dictionary<Language, string> {
                { Language.English, "❌ Uncheck All" },
                { Language.Spanish, "❌ Desmarcar" },
                { Language.French, "❌ Tout Décocher" },
                { Language.German, "❌ Alle Abwählen" },
                { Language.Italian, "❌ Deseleziona Tutto" },
                { Language.Japanese, "❌ すべて解除" }
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
                { Language.English, "❌ Remove Selected" },
                { Language.Spanish, "❌ Eliminar Seleccionadas" },
                { Language.French, "❌ Supprimer Sélectionnées" },
                { Language.German, "❌ Ausgewählte Entfernen" },
                { Language.Italian, "❌ Rimuovi Selezionate" },
                { Language.Japanese, "❌ 選択を削除" }
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
                { Language.English, "✓ Apply to All" },
                { Language.Spanish, "✓ Aplicar a Todas" },
                { Language.French, "✓ Appliquer à Tous" },
                { Language.German, "✓ Auf Alle Anwenden" },
                { Language.Italian, "✓ Applica a Tutti" },
                { Language.Japanese, "✓ すべてに適用" }
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
                { Language.English, "Analyzing files..." },
                { Language.Spanish, "Analizando archivos..." },
                { Language.French, "Analyse des fichiers..." },
                { Language.German, "Dateien werden analysiert..." },
                { Language.Italian, "Analisi dei file..." },
                { Language.Japanese, "ファイルを分析中..." }
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
            { "STATUS_CANCELLED", new Dictionary<Language, string> {
                { Language.English, "Operation cancelled." },
                { Language.Spanish, "Operación cancelada." },
                { Language.French, "Opération annulée." },
                { Language.German, "Operation abgebrochen." },
                { Language.Italian, "Operazione annullata." },
                { Language.Japanese, "操作がキャンセルされました。" }
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
                { Language.English, "HOW TO USE KILOFILTER\n\nQUICK START GUIDE\n\nStep 1: Select Source Folder\n- Click Browse next to SOURCE FOLDER\n- Navigate to the folder containing the files you want to organize\n\nStep 2: Choose Analysis Mode\n\nOption A: ANALYZE DISK (Purple Button)\n- Scans ALL files and categorizes them by type\n- Shows all files including duplicate copies\n- Use when you want to copy everything\n\nOption B: ANALYZE (NO DUPLICATES) (Orange Button)\n- Automatically detects and removes duplicate files\n- Shows only 1 file from each duplicate group\n- Displays a Duplicate Report showing what was found\n- Use when you want to save storage by removing copies\n\nStep 3: Review Results\n- Check the categories list (Images, Videos, Documents, etc.)\n- Each row shows: Category, Number of files, Total size\n- Click View Details to see individual files\n- If using NO DUPLICATES mode, you can Reopen Report\n\nStep 4: Configure (Optional)\n- Click Configure to customize file extensions per category\n- Use BLACKLIST tab to exclude unwanted file types\n- Set minimum file sizes for each extension\n\nStep 5: Create Custom Categories (Optional)\n- Click New Category to create your own file groups\n- Enter name, add extensions, choose to analyze\n\nStep 6: Select Destination Folder\n- Click Browse next to DESTINATION FOLDER\n- Choose where to save organized files\n- New folder created with date/time stamp\n\nStep 7: Rescue Files\n- Check/uncheck categories to copy\n- Click RESCUE SELECTED\n- Files copied to destination in organized folders\n- Original files stay untouched\n\nKEY FEATURES\n- Double-click files to open their location\n- Admin button: Run with administrator privileges for protected folders\n- Language button: Switch between 6 languages\n- Clear button: Reset everything to start over\n- File count matches exactly what will be copied\n\nSMART CACHE SYSTEM (NEW FEATURE)\n\nKiloFilter now includes an intelligent cache system that automatically saves analysis results. When you analyze the same folder again:\n\n- First scan: Full analysis performed, results saved automatically\n- Subsequent scans: If folder content hasn't changed, you're notified and can reload the cached result in <1 second instead of minutes\n- Change detection: The system detects if files were added, deleted, or modified using advanced content hashing\n- Auto-cleanup: Cache automatically removed after 30 days of non-use to prevent disk space accumulation\n\nHow to use:\n-Click the 📋 History button to view all saved analyses\n- Select a previous analysis to load it instantly\n- Delete individual analyses or clear all cache as needed\n- The system handles everything automatically - no user configuration needed\n\nThis feature saves time when working with large folders you analyze repeatedly!\n\nSMART DUPLICATE DELETION (v2.1.0 - NEW!)\n\nThis advanced feature allows you to intelligently delete duplicate files using three different strategies:\n\nThree Deletion Strategies:\n\n1. KEEP NEWEST - Retains the most recently modified file from each duplicate group\n- Perfect when you want the latest version\n- Example: If you have 3 copies of a file from different dates, keeps the newest one\n\n2. KEEP OLDEST - Retains the original/oldest file from each duplicate group\n- Useful for archival and historical purposes\n- Example: Keep the first copy, delete newer copies\n\n3. KEEP SMALLEST - Retains the smallest file from each duplicate group\n- Optimizes storage space maximally\n- Example: Multiple copies of same document at different compressions, keeps most compressed\n\nHow to Use:\n\n1. Click ANALYZE DUPLICATES (orange button) to find all duplicate files\n2. Go to Tab 3 (Search & Filter) for advanced options\n3. (Optional) Apply filters: Enter filename search, set size range, click APPLY FILTERS\n4. Select your Deletion Strategy (Keep Newest/Oldest/Smallest)\n5. Review the Real-Time Preview showing files to delete and space to be freed\n6. Click SMART DELETE to execute\n7. Confirm the deletion\n\nAdvanced Filtering:\n- Filename search by partial name\n- Size range filtering (min/max)\n- Real-time preview updates\n- Exact space calculation\n\nIMPORTANT NOTES:\n- Always review preview before deleting\n- Deleted files are permanent (not Recycle Bin)\n- Strategy applies to ALL duplicate groups\n- Use different strategies to find best option for your needs\n\nIMPORTANT: When using NO DUPLICATES mode, only 1 copy from each duplicate group is copied, saving storage space." },
                { Language.Spanish, "COMO USAR KILOFILTER\n\nGUIA DE INICIO RAPIDO\n\nPaso 1: Seleccionar Carpeta de Origen\n- Haz clic en Examinar junto a CARPETA DE ORIGEN\n- Navega hasta la carpeta que contiene los archivos\n\nPaso 2: Elegir Modo de Analisis\n\nOpcion A: ANALIZAR DISCO (Boton Morado)\n- Examina TODOS los archivos y los categoriza por tipo\n- Muestra todos los archivos incluyendo copias duplicadas\n- Usa cuando quieras copiar todo\n\nOpcion B: ANALIZAR (SIN DUPLICADOS) (Boton Naranja)\n- Detecta y elimina automaticamente archivos duplicados\n- Muestra solo 1 archivo de cada grupo de duplicados\n- Genera un Reporte de Duplicados mostrando lo encontrado\n- Usa cuando quieras ahorrar espacio eliminando copias\n\nPaso 3: Revisar Resultados\n- Revisa la lista de categorias (Imagenes, Videos, Documentos, etc.)\n- Cada fila muestra: Categoria, Cantidad de archivos, Tamanio total\n- Haz clic en Ver Detalle para ver archivos individuales\n- En modo SIN DUPLICADOS, puedes Reabrir Informe\n\nPaso 4: Configurar (Opcional)\n- Haz clic en Configurar para personalizar extensiones por categoria\n- Usa pestaña BLACKLIST para excluir tipos de archivo no deseados\n- Establece tamanios minimos de archivo por extension\n\nPaso 5: Crear Categorias Personalizadas (Opcional)\n- Haz clic en Nueva Categoria para crear tus propios grupos\n- Ingresa nombre, agrega extensiones, elige si analizar\n\nPaso 6: Seleccionar Carpeta de Destino\n- Haz clic en Examinar junto a CARPETA DE DESTINO\n- Elige donde guardar los archivos organizados\n- Se crea nueva carpeta con fecha/hora\n\nPaso 7: Rescatar Archivos\n- Marca/desmarca categorias para copiar\n- Haz clic en RESCATAR SELECCIONADOS\n- Los archivos se copian al destino en carpetas organizadas\n- Los archivos originales permanecen intactos\n\nCARACTERISTICAS PRINCIPALES\n- Doble clic en archivos para abrir su ubicacion\n- Boton Admin: Ejecutar con permisos de administrador para carpetas protegidas\n- Boton Idioma: Cambiar entre 6 idiomas\n- Boton Limpiar: Reiniciar todo\n- La cantidad de archivos coincide exactamente con lo que se copiara\n\nSISTEMA DE CACHE INTELIGENTE (NUEVA CARACTERISTICA)\n\nKiloFilter ahora incluye un sistema de cache inteligente que guarda automaticamente los resultados del analisis. Cuando analizas la misma carpeta nuevamente:\n\n- Primer escaneo: Se realiza analisis completo, resultados guardados automaticamente\n- Escaneos posteriores: Si el contenido de la carpeta no cambio, se te notifica y puedes cargar el resultado cacheado en <1 segundo en lugar de minutos\n- Deteccion de cambios: El sistema detecta si se agregaron, eliminaron o modificaron archivos usando hash de contenido avanzado\n- Limpieza automatica: El cache se elimina automaticamente despues de 30 dias de no usarse para prevenir acumulacion de espacio\n\nComo usar:\n- Haz clic en el boton 📋 Historial para ver todos los analisis guardados\n- Selecciona un analisis anterior para cargarlo instantaneamente\n- Elimina analisis individuales o limpia todo el cache segun sea necesario\n- El sistema maneja todo automaticamente - no se requiere configuracion del usuario\n\n!Esta caracteristica ahorra tiempo cuando trabajas con carpetas grandes que analizas repetidamente!\n\nELIMINACION INTELIGENTE DE DUPLICADOS (v2.1.0 - ¡NUEVO!)\n\nEsta caracteritica avanzada te permite eliminar inteligentemente archivos duplicados usando tres estrategias diferentes:\n\nTres Estrategias de Eliminacion:\n\n1. MANTENER MAS RECIENTE - Retiene el archivo modificado mas recientemente de cada grupo de duplicados\n- Perfecto cuando quieres la version mas reciente\n- Ejemplo: Si tienes 3 copias de un archivo de diferentes fechas, mantiene la mas nueva\n\n2. MANTENER MAS ANTIGUO - Retiene el archivo original/mas antiguo de cada grupo de duplicados\n- Util para propositos de archivo e historicos\n- Ejemplo: Mantener la primera copia, eliminar copias mas nuevas\n\n3. MANTENER MAS PEQUENO - Retiene el archivo mas pequeno de cada grupo de duplicados\n- Optimiza al maximo el espacio de almacenamiento\n- Ejemplo: Multiples copias del mismo documento a diferentes compresiones, mantiene la mas comprimida\n\nComo usar:\n\n1. Haz clic en ANALIZAR DUPLICADOS (boton naranja) para encontrar todos los archivos duplicados\n2. Ve a la Pestana 3 (Buscar y Filtrar) para opciones avanzadas\n3. (Opcional) Aplica filtros: Ingresa busqueda de nombre, establece rango de tamanio, haz clic en APLICAR FILTROS\n4. Selecciona tu Estrategia de Eliminacion (Mantener Mas Reciente/Antiguo/Pequeno)\n5. Revisa la Vista Previa en Tiempo Real mostrando archivos a eliminar y espacio a liberar\n6. Haz clic en ELIMINAR INTELIGENTEMENTE para ejecutar\n7. Confirma la eliminacion\n\nFiltrado Avanzado:\n- Busqueda de nombre por nombre parcial\n- Filtrado por rango de tamanio (min/max)\n- Actualizaciones de vista previa en tiempo real\n- Calculo de espacio exacto\n\nNOTAS IMPORTANTES:\n- Siempre revisa la vista previa antes de eliminar\n- Los archivos eliminados son permanentes (no van a Papelera)\n- La estrategia se aplica a TODOS los grupos de duplicados\n- Usa diferentes estrategias para encontrar la mejor opcion para tus necesidades\n\nIMPORTANTE: En modo SIN DUPLICADOS, solo se copia 1 archivo de cada grupo de duplicados, ahorrando espacio de almacenamiento." },
                { Language.French, "COMMENT UTILISER KILOFILTER\n\nGUIDDE DE DEMARRAGE RAPIDE\n\nEtape 1: Selectionner Dossier Source\n- Cliquez sur Parcourir a cote de DOSSIER SOURCE\n- Naviguez jusqu'au dossier contenant les fichiers\n\nEtape 2: Choisir Mode d'Analyse\n\nOption A: ANALYSER LE DISQUE (Bouton Violet)\n- Analyse TOUS les fichiers et les categorise par type\n- Affiche tous les fichiers y compris les doublons\n- Utilisez quand vous voulez tout copier\n\nOption B: ANALYSER (SANS DOUBLONS) (Bouton Orange)\n- Detecte et supprime automatiquement les fichiers en double\n- Affiche seulement 1 fichier de chaque groupe de doublons\n- Genere un Rapport de Doublons montrant ce qui a ete trouve\n- Utilisez pour economiser de l'espace en supprimant les copies\n\nEtape 3: Examiner les Resultats\n- Verifiez la liste des categories (Images, Videos, Documents, etc.)\n- Chaque ligne affiche: Categorie, Nombre de fichiers, Taille totale\n- Cliquez sur Voir Details pour voir les fichiers individuels\n- En mode SANS DOUBLONS, vous pouvez Rouvrir Rapport\n\nEtape 4: Configurer (Optionnel)\n- Cliquez sur Configurer pour personnaliser extensions par categorie\n- Utilisez onglet LISTE NOIRE pour exclure types de fichiers non desires\n- Definissez tailles minimales de fichier par extension\n\nEtape 5: Creer Categories Personnalisees (Optionnel)\n- Cliquez sur Nouvelle Categorie pour creer vos propres groupes\n- Entrez nom, ajoutez extensions, choisissez d'analyser\n\nEtape 6: Selectionner Dossier de Destination\n- Cliquez sur Parcourir a cote de DOSSIER DE DESTINATION\n- Choisissez ou enregistrer les fichiers organises\n- Nouveau dossier cree avec date/heure\n\nEtape 7: Sauvegarder Fichiers\n- Cochez/decochez categories a copier\n- Cliquez sur SAUVEGARDER SELECTIONNES\n- Les fichiers sont copies au destination dans dossiers organises\n- Les fichiers originaux restent intacts\n\nCARACTERISTIQUES PRINCIPALES\n- Double-cliquez sur fichiers pour ouvrir emplacement\n- Bouton Admin: Executer avec privileges administrateur pour dossiers proteges\n- Bouton Langue: Changer entre 6 langues\n- Bouton Effacer: Reinitialiser tout\n- Le nombre de fichiers correspond exactement a ce qui sera copie\n\nSYSTEME DE CACHE INTELLIGENT (NOUVELLE FONCTIONNALITE)\n\nKiloFilter inclut maintenant un systeme de cache intelligent qui enregistre automatiquement les resultats d'analyse. Lorsque vous analysez le meme dossier a nouveau:\n\n- Premier scan: Analyse complete effectuee, resultats enregistres automatiquement\n- Scans subsequents: Si le contenu du dossier n'a pas change, vous etes notifie et pouvez charger le resultat en cache en <1 seconde au lieu de minutes\n- Detection de changement: Le systeme detecte si des fichiers ont ete ajoutes, supprimes ou modifies en utilisant un hachage de contenu avance\n- Nettoyage automatique: Le cache est automatiquement supprime apres 30 jours de non-utilisation pour eviter l'accumulation d'espace disque\n\nComment utiliser:\n- Cliquez sur le bouton 📋 Historique pour afficher toutes les analyses enregistrees\n- Selectionnez une analyse anterieure pour la charger instantanement\n- Supprimez des analyses individuelles ou videz tout le cache selon vos besoins\n- Le systeme gere tout automatiquement - aucune configuration utilisateur necessaire\n\nCette fonctionnalite economise du temps lorsque vous travaillez avec de grands dossiers que vous analysez repetitivement!\n\nSUPPRESSION INTELLIGENTE DES DOUBLONS (v2.1.0 - NOUVEAU!)\n\nCette fonctionnalite avancee vous permet de supprimer intelligemment les fichiers en double en utilisant trois strategies differentes:\n\nTrois Strategies de Suppression:\n\n1. CONSERVER LE PLUS RECENT - Conserve le fichier modifie le plus recemment de chaque groupe de doublons\n- Parfait quand vous voulez la version la plus recente\n- Exemple: Si vous avez 3 copies d'un fichier de differentes dates, conserve la plus recente\n\n2. CONSERVER LE PLUS ANCIEN - Conserve le fichier original/le plus ancien de chaque groupe de doublons\n- Utile pour les buts d'archivage et historiques\n- Exemple: Conserver la premiere copie, supprimer les copies plus recentes\n\n3. CONSERVER LE PLUS PETIT - Conserve le plus petit fichier de chaque groupe de doublons\n- Optimise au maximum l'espace de stockage\n- Exemple: Plusieurs copies du meme document a differentes compressions, conserve la plus comprimee\n\nComment utiliser:\n\n1. Cliquez sur ANALYSER LES DOUBLONS (bouton orange) pour trouver tous les fichiers en double\n2. Allez a l'Onglet 3 (Recherche et Filtrage) pour les options avancees\n3. (Optionnel) Appliquez les filtres: Entrez recherche de nom, definissez plage de taille, cliquez sur APPLIQUER LES FILTRES\n4. Selectionnez votre strategie de suppression souhaitee (Conserver Plus Recent/Ancien/Petit)\n5. Examinez l'Apercu en Temps Reel affichant fichiers a supprimer et espace a liberer\n6. Cliquez sur SUPPRIMER INTELLIGEMMENT pour executer\n7. Confirmez la suppression\n\nFiltrage Avances:\n- Recherche de nom par nom partiel\n- Filtrage par plage de taille (min/max)\n- Mises a jour d'apercu en temps reel\n- Calcul d'espace exact\n\nNOTES IMPORTANTES:\n- Examinez toujours l'apercu avant de supprimer\n- Les fichiers supprimes sont permanents (non places a la Corbeille)\n- La strategie s'applique a TOUS les groupes de doublons\n- Utilisez differentes strategies pour trouver la meilleure option pour vos besoins\n\nIMPORTANT: En mode SANS DOUBLONS, une seule copie de chaque groupe de doublons est copiee, economisant l'espace de stockage." },
                { Language.German, "SO VERWENDEN SIE KILOFILTER\n\nSCHNELLSTARTANLEITUNG\n\nSchritt 1: Quellordner Auswaehlen\n- Klicken Sie auf Durchsuchen neben QUELLORDNER\n- Navigieren Sie zu Ordner mit Dateien\n\nSchritt 2: Analysemodus Auswaehlen\n\nOption A: FESTPLATTE ANALYSIEREN (Violetter Button)\n- Abtastung ALLER Dateien und Kategorisierung nach Typ\n- Zeigt alle Dateien einschliesslich Duplikate\n- Verwenden Sie, wenn Sie alles kopieren moechten\n\nOption B: ANALYSIEREN (KEINE DUPLIKATE) (Oranger Button)\n- Erkennt und entfernt automatisch doppelte Dateien\n- Zeigt nur 1 Datei pro Duplikatgruppe an\n- Erstellt Duplikatbericht mit Angaben\n- Verwenden um Speicher durch Entfernen von Kopien zu sparen\n\nSchritt 3: Ergebnisse Ueberprufen\n- Pruefen Sie Kategorieliste (Bilder, Videos, Dokumente, etc.)\n- Jede Zeile zeigt: Kategorie, Dateizahl, Gesamtgroesse\n- Klicken Sie auf Details Anzeigen fuer individuelle Dateien\n- Im Modus KEINE DUPLIKATE koennen Sie Bericht Erneut Oeffnen\n\nSchritt 4: Konfigurieren (Optional)\n- Klicken Sie auf Konfigurieren um Erweiterungen per Kategorie anzupassen\n- Verwenden Sie BLACKLIST Registerkarte fuer unwuenschte Dateitypen\n- Legen Sie Mindestdateien per Erweiterung fest\n\nSchritt 5: Benutzerdefinierte Kategorien Erstellen (Optional)\n- Klicken Sie auf Neue Kategorie fuer eigene Gruppen\n- Geben Sie Namen ein, fuegen Sie Erweiterungen hinzu\n\nSchritt 6: Zielordner Auswaehlen\n- Klicken Sie auf Durchsuchen neben ZIELORDNER\n- Waehlen Sie wo organisierte Dateien gespeichert werden\n- Neuer Ordner mit Datum/Uhrzeit erstellt\n\nSchritt 7: Dateien Retten\n- Aktivieren/Deaktivieren Sie zu kopierende Kategorien\n- Klicken Sie auf AUSGEWA HLTE RETTEN\n- Dateien werden zu Ziel in organisierten Ordnern kopiert\n- Urspruengliche Dateien bleiben unberuehrt\n\nHAUPTMERKMALE\n- Doppelklick auf Dateien um Speicherort zu oeffnen\n- Admin Button: Mit Administratorbefugnissen fuer geschuetzte Ordner\n- Sprachbutton: Zwischen 6 Sprachen waehlen\n- Loeschbutton: Alles zuruecksetzen\n- Dateizahl entspricht genau was kopiert wird\n\nINTELLIGENTES CACHE-SYSTEM (NEUE FUNKTION)\n\nKiloFilter enthaelt nun ein intelligentes Cache-System, das Analyseergebnisse automatisch speichert. Wenn Sie denselben Ordner erneut analysieren:\n\n- Erstes Scannen: Vollstaendige Analyse durchgefuehrt, Ergebnisse automatisch gespeichert\n- Nachfolgendes Scannen: Falls Ordnerinhalt nicht geaendert hat, werden Sie benachrichtigt und koennen das gecachte Ergebnis in <1 Sekunde statt Minuten laden\n- Aenderungserkennung: Das System erkennt, ob Dateien mithilfe erweitertes Content-Hashing hinzugefuegt, geloescht oder geaendert wurden\n- Automatische Bereinigung: Cache wird automatisch nach 30 Tagen Nichtbenutzung entfernt, um Speicherplatzansammlung zu vermeiden\n\nSo verwenden Sie es:\n- Klicken Sie auf die Schaltflaeche 📋 Verlauf um alle gespeicherten Analysen anzuzeigen\n- Waehlen Sie eine vorherige Analyse aus um sie sofort zu laden\n- Loeschen Sie einzelne Analysen oder leeren Sie den gesamten Cache nach Bedarf\n- Das System verwaltet alles automatisch - keine Benutzerkonfiguration erforderlich\n\nDiese Funktion spart Zeit bei der Arbeit mit grossen Ordnern, die Sie wiederholt analysieren!\n\nINTELLIGENTE DUPLIKATLOESCHUNG (v2.1.0 - NEU!)\n\nDiese erweiterte Funktion ermoeglicht es Ihnen, Duplikatdateien intelligent mit drei verschiedenen Strategien zu loeschen:\n\nDrei Loeschstrategien:\n\n1. NEUESTE BEHALTEN - Behaelt die zuletzt geaenderte Datei aus jeder Duplikatgruppe\n- Perfekt wenn Sie die neueste Version moechten\n- Beispiel: Wenn Sie 3 Kopien einer Datei von verschiedenen Daten haben, behaelt die neueste\n\n2. ALTESTE BEHALTEN - Behaelt die urspruengliche/alteste Datei aus jeder Duplikatgruppe\n- Nuetzlich fuer Archiv- und historische Zwecke\n- Beispiel: Erste Kopie behalten, neuere Kopien loeschen\n\n3. KLEINSTE BEHALTEN - Behaelt die kleinste Datei aus jeder Duplikatgruppe\n- Optimiert den Speicherplatz maximal\n- Beispiel: Mehrere Kopien desselben Dokuments bei unterschiedlichen Kompressionen, behaelt am meisten komprimiert\n\nSo verwenden Sie es:\n\n1. Klicken Sie auf DUPLIKATE ANALYSIEREN (orangerer Button) um alle Duplikatdateien zu finden\n2. Gehen Sie zu Registerkarte 3 (Suche & Filterung) fuer erweiterte Optionen\n3. (Optional) Filtrer anwenden: Geben Sie Namensuche ein, legen Sie Groessenbereich fest, klicken Sie auf FILTER ANWENDEN\n4. Waehlen Sie Ihre gewuenschte Loeschstrategie (Neueste/Alteste/Kleinste Behalten)\n5. Ueberpruefen Sie die Echtzeitvorschau zeigt Dateien zum Loeschen und freizugebenden Speicherplatz\n6. Klicken Sie auf INTELLIGENT LOESCHEN um auszufuehren\n7. Bestaetigen Sie die Loeschung\n\nErweiterte Filterung:\n- Namensuche nach Teilnamen\n- Groessenbereich-Filterung (min/max)\n- Echtzeitvorschau-Aktualisierungen\n- Genaue Speicherplatzberechnung\n\nWICHTIGE HINWEISE:\n- Ueberpruefen Sie immer die Vorschau vor dem Loeschen\n- Geloeschte Dateien sind permanent (nicht im Papierkorb)\n- Die Strategie gilt fuer ALLE Duplikatgruppen\n- Verwenden Sie verschiedene Strategien um die beste Option zu finden\n\nWICHTIG: Im Modus KEINE DUPLIKATE wird nur 1 Kopie pro Duplikatgruppe kopiert, was Speicherplatz spart." },
                { Language.Italian, "COME USARE KILOFILTER\n\nGUIDEA RAPIDA\n\nPassaggio 1: Selezionare Cartella Sorgente\n- Clicca su Sfoglia accanto a CARTELLA SORGENTE\n- Naviga fino alla cartella con i file\n\nPassaggio 2: Scegliere Modalita di Analisi\n\nOpzione A: ANALIZZA DISCO (Pulsante Viola)\n- Scansiona TUTTI i file e li categorizza per tipo\n- Mostra tutti i file inclusi i duplicati\n- Usa quando vuoi copiare tutto\n\nOpzione B: ANALIZZA (SENZA DUPLICATI) (Pulsante Arancione)\n- Rileva ed elimina automaticamente file duplicati\n- Mostra solo 1 file da ogni gruppo di duplicati\n- Genera Rapporto Duplicati con dettagli\n- Usa per risparmiare spazio rimuovendo copie\n\nPassaggio 3: Rivedere i Risultati\n- Controlla lista categorie (Immagini, Video, Documenti, etc.)\n- Ogni riga mostra: Categoria, Numero file, Dimensione totale\n- Clicca su Visualizza Dettagli per file individuali\n- In modalita SENZA DUPLICATI puoi Riapri Report\n\nPassaggio 4: Configurare (Opzionale)\n- Clicca su Configura per personalizzare estensioni per categoria\n- Usa scheda BLACKLIST per escludere tipi indesiderati\n- Imposta dimensioni minime per estensione\n\nPassaggio 5: Creare Categorie Personalizzate (Opzionale)\n- Clicca su Nuova Categoria per creare tuoi gruppi\n- Inserisci nome, aggiungi estensioni, scegli analisi\n\nPassaggio 6: Selezionare Cartella Destinazione\n- Clicca su Sfoglia accanto a CARTELLA DI DESTINAZIONE\n- Scegli dove salvare file organizzati\n- Nuova cartella creata con data/ora\n\nPassaggio 7: Salvare File\n- Seleziona/deseleziona categorie da copiare\n- Clicca su SALVA SELEZIONATI\n- I file sono copiati a destinazione in cartelle organizzate\n- I file originali rimangono intatti\n\nCARATTERISTICHE PRINCIPALI\n- Doppio clic su file per aprire posizione\n- Pulsante Admin: Esegui con privilegi amministrativi per cartelle protette\n- Pulsante Lingua: Scegli tra 6 lingue\n- Pulsante Pulisci: Ripristina tutto\n- Il numero di file corrisponde esattamente a cosa sara copiato\n\nSISTEMA CACHE INTELLIGENTE (NUOVA FUNZIONE)\n\nKiloFilter ora include un sistema cache intelligente che salva automaticamente i risultati dell'analisi. Quando analizzi di nuovo la stessa cartella:\n\n- Prima scansione: Analisi completa eseguita, risultati salvati automaticamente\n- Scansioni successive: Se il contenuto della cartella non e cambato, vieni notificato e puoi ricaricare il risultato in cache in <1 secondo invece di minuti\n- Rilevamento modifiche: Il sistema rileva se i file sono stati aggiunti, eliminati o modificati utilizzando hashing avanzato del contenuto\n- Pulizia automatica: La cache viene rimossa automaticamente dopo 30 giorni di non utilizzo per prevenire accumulo di spazio su disco\n\nCome usare:\n- Clicca sul pulsante 📋 Cronologia per visualizzare tutte le analisi salvate\n- Seleziona un'analisi precedente per caricarla istantaneamente\n- Elimina singole analisi o cancella tutta la cache secondo necessita\n- Il sistema gestisce tutto automaticamente - nessuna configurazione utente richiesta\n\nQuesta funzione risparmia tempo quando lavori con cartelle grandi che analizzi ripetutamente!\n\nELIMINAZIONE INTELLIGENTE DI DUPLICATI (v2.1.0 - NUOVO!)\n\nQuesta funzione avanzata ti consente di eliminare intelligentemente i file duplicati utilizzando tre diverse strategie:\n\nTre Strategie di Eliminazione:\n\n1. MANTIENI PIU RECENTE - Mantiene il file modificato piu di recente da ogni gruppo di duplicati\n- Perfetto quando vuoi la versione piu recente\n- Esempio: Se hai 3 copie di un file da date diverse, mantiene la piu nuova\n\n2. MANTIENI PIU ANTICO - Mantiene il file originale/piu antico da ogni gruppo di duplicati\n- Utile per scopi di archiviazione e storici\n- Esempio: Mantieni la prima copia, elimina le copie piu nuove\n\n3. MANTIENI PIU PICCOLO - Mantiene il file piu piccolo da ogni gruppo di duplicati\n- Ottimizza al massimo lo spazio di archiviazione\n- Esempio: Piu copie dello stesso documento a compressioni diverse, mantiene la piu compressa\n\nCome utilizzare:\n\n1. Fai clic su ANALIZZA DUPLICATI (pulsante arancione) per trovare tutti i file duplicati\n2. Vai alla Scheda 3 (Ricerca e Filtro) per le opzioni avanzate\n3. (Opzionale) Applica filtri: Inserisci ricerca del nome, imposta intervallo di dimensioni, fai clic su APPLICA FILTRI\n4. Seleziona la tua strategia di eliminazione desiderata (Mantieni Piu Recente/Antico/Piccolo)\n5. Esamina l'Anteprima in Tempo Reale mostrando file da eliminare e spazio da liberare\n6. Fai clic su ELIMINA INTELLIGENTEMENTE per eseguire\n7. Conferma l'eliminazione\n\nFiltri Avanzati:\n- Ricerca per nome parziale\n- Filtri per intervallo di dimensioni (min/max)\n- Aggiornamenti anteprima in tempo reale\n- Calcolo spazio esatto\n\nNOTE IMPORTANTI:\n- Esamina sempre l'anteprima prima di eliminare\n- I file eliminati sono permanenti (non nel Cestino)\n- La strategia si applica a TUTTI i gruppi di duplicati\n- Usa diverse strategie per trovare l'opzione migliore per le tue esigenze\n\nIMPORTANTE: In modalita SENZA DUPLICATI, una sola copia da ogni gruppo di duplicati viene copiata, risparmiando spazio di archiviazione." },
                { Language.Japanese, "KILOFILTER NO TSUKAIKATA\n\nQUICK START GUIDE\n\nステップ1: ソースフォルダーを選択\n- ソースフォルダーの横の参照をクリック\n- ファイルが含まれているフォルダーに移動\n\nステップ2: 分析モードを選択\n\nオプションA: ディスク分析（紫ボタン）\n- すべてのファイルをスキャンタイプ別に分類\n- 重複を含むすべてのファイルを表示\n- すべてをコピーしたい場合に使用\n\nオプションB: 分析（重複なし）（橙ボタン）\n- 重複ファイルを自動検出して削除\n- 各グループから1ファイルのみ表示\n- 重複レポートを生成\n- ストレージ容量削減で重複コピー削除\n\nステップ3: 結果を確認\n- カテゴリーリスト（画像、動画、ドキュメント等）を確認\n- 各行表示: カテゴリー、ファイル数、合計サイズ\n- 詳細表示で個別ファイルを表示\n- 重複なしモード: レポート再度開く\n\nステップ4: 設定（オプション）\n- 設定をクリックしてカテゴリー別拡張子をカスタマイズ\n- ブラックリストタブで不要なタイプを除外\n- 拡張子別の最小ファイルサイズ設定\n\nステップ5: カスタムカテゴリー作成（オプション）\n- 新しいカテゴリーをクリックして独自グループ作成\n- 名前入力、拡張子追加、分析選択\n\nステップ6: 保存先フォルダー選択\n- 保存先フォルダーの横の参照をクリック\n- 整理ファイルの保存場所を選択\n- 日付/時刻でフォルダー作成\n\nステップ7: ファイル救出\n- コピー対象をチェック/チェック解除\n- 選択を救出をクリック\n- ファイルは整理フォルダーにコピー\n- 元のファイルは変更なし\n\n主な機能\n- ダブルクリックでファイル位置を開く\n- 管理者ボタン: 保護フォルダーに管理者権限\n- 言語ボタン: 6言語から選択\n- クリアボタン: すべてをリセット\n- ファイル数はコピーされます\n\nインテリジェント キャッシュ システム（新機能）\n\nKiloFilterには、分析結果を自動的に保存するインテリジェント キャッシュ システムが含まれるようになりました。同じフォルダーを再度分析するとき:\n\n- 最初のスキャン: 完全な分析が実行され、結果は自動的に保存されます\n- その後のスキャン: フォルダーの内容が変わっていない場合、通知が送られて、キャッシュされた結果が数分ではなく1秒以内で読み込めます\n- 変更検出: システムは高度なコンテンツ ハッシュを使用してファイルが追加、削除、または変更されたかを検出\n- 自動クリーンアップ: キャッシュは30日間の未使用後に自動的に削除され、ディスク容量の蓄積を防ぎます\n\n使用方法:\n- 📋 履歴ボタンをクリックして、保存されたすべての分析を表示\n- 前の分析を選択して即座に読み込む\n- 個別の分析を削除するか、必要に応じてすべてのキャッシュをクリア\n- システムがすべてを自動的に処理します - ユーザー設定は不要\n\nこの機能は、繰り返し分析する大きなフォルダーで作業するときに時間を節約します!\n\nインテリジェント重複削除 (v2.1.0 - 新機能!)\n\nこの高度な機能により、3つの異なる戦略を使用して重複ファイルをインテリジェントに削除できます:\n\n3つの削除戦略:\n\n1. 最新を保持 - 各重複グループから最後に修正されたファイルを保持\n- 最新バージョンが必要な場合に最適\n- 例: 異なる日付の同じファイルのコピーが3つある場合、最新を保持\n\n2. 最古を保持 - 各重複グループから元の/最も古いファイルを保持\n- アーカイブおよび履歴目的に有用\n- 例: 最初のコピーを保持、新しいコピーを削除\n\n3. 最小を保持 - 各重複グループから最も小さいファイルを保持\n- ストレージ容量を最大限に最適化\n- 例: 異なる圧縮率の同じドキュメントの複数コピー、最も圧縮されたものを保持\n\n使用方法:\n\n1. 重複分析をクリック（オレンジボタン）すべての重複ファイルを検索\n2. タブ3（検索とフィルター）で詳細オプションに移動\n3. （オプション）フィルターを適用: ファイル名検索を入力、サイズ範囲を設定、フィルター適用をクリック\n4. 希望する削除戦略を選択（最新/最古/最小を保持）\n5. リアルタイムプレビューを確認 削除するファイルと解放するスペースを表示\n6. インテリジェント削除をクリックして実行\n7. 削除を確認\n\n高度なフィルタリング:\n- 部分的な名前で名前検索\n- サイズ範囲フィルター（最小/最大）\n- リアルタイムプレビュー更新\n- 正確なスペース計算\n\n重要な注意:\n- 削除する前に必ずプレビューを確認してください\n- 削除されたファイルは永続的です（ごみ箱にはありません）\n- 選択した戦略はすべての重複グループに適用されます\n- さまざまな戦略を使用して最適なオプションを見つけてください\n\n重要: 重複なしモードでは各グループから1コピーのみコピーされ、ストレージ削減。" }
            }},
            { "BTN_ADMIN", new Dictionary<Language, string> {
                { Language.English, "👑 Admin (Optional)" },
                { Language.Spanish, "👑 Admin (Opcional)" },
                { Language.French, "👑 Admin (Optionnel)" },
                { Language.German, "👑 Admin (Optional)" },
                { Language.Italian, "👑 Admin (Facoltativo)" },
                { Language.Japanese, "👑 管理者 (オプション)" }
            }},
            { "BTN_HELP", new Dictionary<Language, string> {
                { Language.English, "❓ Help" },
                { Language.Spanish, "❓ Ayuda" },
                { Language.French, "❓ Aide" },
                { Language.German, "❓ Hilfe" },
                { Language.Italian, "❓ Aiuto" },
                { Language.Japanese, "❓ ヘルプ" }
            }},
            { "ALREADY_ADMIN", new Dictionary<Language, string> {
                { Language.English, "The program is already running with administrator privileges." },
                { Language.Spanish, "El programa ya se está ejecutando con permisos de administrador." },
                { Language.French, "Le programme s'exécute déjà avec les privilèges d'administrateur." },
                { Language.German, "Das Programm wird bereits mit Administratorrechten ausgeführt." },
                { Language.Italian, "Il programma è già in esecuzione con privilegi di amministratore." },
                { Language.Japanese, "プログラムは既に管理者権限で実行されています。" }
            }},
            { "ADMIN_DENIED", new Dictionary<Language, string> {
                { Language.English, "Admin request was denied or cancelled. Please try again with administrator privileges." },
                { Language.Spanish, "La solicitud de administrador fue rechazada o cancelada. Intente de nuevo con permisos de administrador." },
                { Language.French, "La demande d'administrateur a été refusée ou annulée. Réessayez avec les privilèges d'administrateur." },
                { Language.German, "Die Administratoreinstellung wurde verweigert oder abgebrochen. Versuchen Sie es erneut mit Administratorrechten." },
                { Language.Italian, "La richiesta di amministratore è stata rifiutata o annullata. Riprovare con privilegi di amministratore." },
                { Language.Japanese, "管理者リクエストが拒否またはキャンセルされました。管理者権限で再度お試しください。" }
            }},
            { "BTN_ANALYZE_DUPLICATES", new Dictionary<Language, string> {
                { Language.English, "⛏ ANALYZE (NO DUPLICATES)" },
                { Language.Spanish, "⛏ ANALIZAR (SIN DUPLICADOS)" },
                { Language.French, "⛏ ANALYSER (SANS DOUBLONS)" },
                { Language.German, "⛏ ANALYSIEREN (KEINE DUPLIKATE)" },
                { Language.Italian, "⛏ ANALIZZA (SENZA DUPLICATI)" },
                { Language.Japanese, "⛏分析（重複なし）" }
            }},
            { "STATUS_ANALYZING_DUPLICATES", new Dictionary<Language, string> {
                { Language.English, "Analyzing files (excluding duplicates)..." },
                { Language.Spanish, "Analizando archivos (sin duplicados)..." },
                { Language.French, "Analyse des fichiers (excluant les doublons)..." },
                { Language.German, "Dateien werden analysiert (Duplikate ausgeschlossen)..." },
                { Language.Italian, "Analisi dei file (escludendo i duplicati)..." },
                { Language.Japanese, "ファイルを分析中（重複を除外）..." }
            }},
            { "STATUS_DUPLICATES_FOUND", new Dictionary<Language, string> {
                { Language.English, "Found {0} duplicate files" },
                { Language.Spanish, "Se encontraron {0} archivos duplicados" },
                { Language.French, "Trouvé {0} fichiers en double" },
                { Language.German, "{0} doppelte Dateien gefunden" },
                { Language.Italian, "Trovati {0} file duplicati" },
                { Language.Japanese, "{0}個の重複ファイルが見つかりました" }
            }},
            { "BTN_REOPEN_REPORT", new Dictionary<Language, string> {
                { Language.English, "↻ Reopen Report" },
                { Language.Spanish, "↻ Reabrir Informe" },
                { Language.French, "↻ Rouvrir Rapport" },
                { Language.German, "↻ Bericht Erneut Öffnen" },
                { Language.Italian, "↻ Riapri Report" },
                { Language.Japanese, "↻ レポートを再度開く" }
            }},
            { "DUPLICATES_REPORT_TITLE", new Dictionary<Language, string> {
                { Language.English, "Duplicate Files Report Found" },
                { Language.Spanish, "Reporte de Duplicados Encontrados" },
                { Language.French, "Rapport des Fichiers en Double Trouvés" },
                { Language.German, "Bericht der gefundenen doppelten Dateien" },
                { Language.Italian, "Rapporto sui File Duplicati Trovati" },
                { Language.Japanese, "見つかった重複ファイルレポート" }
            }},
            { "DUPLICATES_FOUND", new Dictionary<Language, string> {
                { Language.English, "Found {0} duplicate group(s)" },
                { Language.Spanish, "Se encontraron {0} grupo(s) de duplicados" },
                { Language.French, "Trouvé {0} groupe(s) de fichiers en double" },
                { Language.German, "{0} doppelte Gruppe(n) gefunden" },
                { Language.Italian, "Trovati {0} gruppo(i) di file duplicati" },
                { Language.Japanese, "{0}個の重複ファイルグループが見つかりました" }
            }},
            { "TAB_SUMMARY_GROUPS", new Dictionary<Language, string> {
                { Language.English, "Summary by Group" },
                { Language.Spanish, "Resumen por Grupo" },
                { Language.French, "Résumé par Groupe" },
                { Language.German, "Zusammenfassung nach Gruppe" },
                { Language.Italian, "Riepilogo per Gruppo" },
                { Language.Japanese, "グループ別サマリー" }
            }},
            { "TAB_DETAILS_FILES", new Dictionary<Language, string> {
                { Language.English, "File Details" },
                { Language.Spanish, "Detalles por Archivo" },
                { Language.French, "Détails des Fichiers" },
                { Language.German, "Dateidetails" },
                { Language.Italian, "Dettagli dei File" },
                { Language.Japanese, "ファイルの詳細" }
            }},
            { "COL_HASH_GROUP", new Dictionary<Language, string> {
                { Language.English, "Hash (Group)" },
                { Language.Spanish, "Hash (Grupo)" },
                { Language.French, "Hash (Groupe)" },
                { Language.German, "Hash (Gruppe)" },
                { Language.Italian, "Hash (Gruppo)" },
                { Language.Japanese, "ハッシュ（グループ）" }
            }},
            { "COL_FILENAME", new Dictionary<Language, string> {
                { Language.English, "File Name" },
                { Language.Spanish, "Nombre Archivo" },
                { Language.French, "Nom du Fichier" },
                { Language.German, "Dateiname" },
                { Language.Italian, "Nome del File" },
                { Language.Japanese, "ファイル名" }
            }},
            { "COL_FULL_PATH", new Dictionary<Language, string> {
                { Language.English, "Full Path" },
                { Language.Spanish, "Ruta Completa" },
                { Language.French, "Chemin Complet" },
                { Language.German, "Vollständiger Pfad" },
                { Language.Italian, "Percorso Completo" },
                { Language.Japanese, "フルパス" }
            }},
            { "COL_DUPLICATE_FILES", new Dictionary<Language, string> {
                { Language.English, "Duplicate Files" },
                { Language.Spanish, "Archivos Duplicados" },
                { Language.French, "Fichiers en Double" },
                { Language.German, "Doppelte Dateien" },
                { Language.Italian, "File Duplicati" },
                { Language.Japanese, "重複ファイル" }
            }},
            { "COL_WASTED_SPACE", new Dictionary<Language, string> {
                { Language.English, "Wasted Space" },
                { Language.Spanish, "Espacio Desperdiciado" },
                { Language.French, "Espace Gaspillé" },
                { Language.German, "Verschwendeter Speicherplatz" },
                { Language.Italian, "Spazio Sprecato" },
                { Language.Japanese, "無駄になったスペース" }
            }},
            { "COL_FILE_NAMES", new Dictionary<Language, string> {
                { Language.English, "File Names" },
                { Language.Spanish, "Nombres" },
                { Language.French, "Noms des Fichiers" },
                { Language.German, "Dateiname" },
                { Language.Italian, "Nomi dei File" },
                { Language.Japanese, "ファイル名" }
            }},
            { "BTN_COPY_CLIPBOARD", new Dictionary<Language, string> {
                { Language.English, "⧉ Copy to Clipboard" },
                { Language.Spanish, "⧉ Copiar al Portapapeles" },
                { Language.French, "⧉ Copier dans le Presse-papiers" },
                { Language.German, "⧉ In Zwischenablage kopieren" },
                { Language.Italian, "⧉ Copia negli Appunti" },
                { Language.Japanese, "⧉ クリップボードにコピー" }
            }},
            { "LABEL_TOTAL", new Dictionary<Language, string> {
                { Language.English, "TOTAL" },
                { Language.Spanish, "TOTAL" },
                { Language.French, "TOTAL" },
                { Language.German, "GESAMT" },
                { Language.Italian, "TOTALE" },
                { Language.Japanese, "合計" }
            }},
            { "BTN_CLOSE", new Dictionary<Language, string> {
                { Language.English, "☓ Close" },
                { Language.Spanish, "☓ Cerrar" },
                { Language.French, "☓ Fermer" },
                { Language.German, "☓ Schließen" },
                { Language.Italian, "☓ Chiudi" },
                { Language.Japanese, "☓ 閉じる" }
            }},
            { "BTN_HISTORY", new Dictionary<Language, string> {
                { Language.English, "📋 Analysis History" },
                { Language.Spanish, "📋 Historial de Análisis" },
                { Language.French, "📋 Historique d'Analyse" },
                { Language.German, "📋 Analyseverlauf" },
                { Language.Italian, "📋 Cronologia Analisi" },
                { Language.Japanese, "📋 分析履歴" }
            }},
            { "CACHE_LOADED", new Dictionary<Language, string> {
                { Language.English, "✅ Analysis loaded from cache" },
                { Language.Spanish, "✅ Análisis cargado desde caché" },
                { Language.French, "✅ Analyse chargée à partir du cache" },
                { Language.German, "✅ Analyse aus Cache geladen" },
                { Language.Italian, "✅ Analisi caricata dalla cache" },
                { Language.Japanese, "✅ キャッシュから分析を読み込みました" }
            }},
            { "CACHE_FOUND", new Dictionary<Language, string> {
                { Language.English, "📊 Previous analysis found" },
                { Language.Spanish, "📊 Análisis anterior encontrado" },
                { Language.French, "📊 Analyse antérieure trouvée" },
                { Language.German, "📊 Frühere Analyse gefunden" },
                { Language.Italian, "📊 Analisi precedente trovata" },
                { Language.Japanese, "📊 前回の分析が見つかりました" }
            }},
            { "FEATURE_CACHE", new Dictionary<Language, string> {
                { Language.English, "Smart Cache System: Analyzes are automatically saved and reused for faster results on repeated scans." },
                { Language.Spanish, "Sistema de Caché Inteligente: Los análisis se guardan automáticamente y se reutilizan para resultados más rápidos en escaneos repetidos." },
                { Language.French, "Système de Cache Intelligent: Les analyses sont automatiquement enregistrées et réutilisées pour des résultats plus rapides lors des analyses répétées." },
                { Language.German, "Intelligentes Cache-System: Analysen werden automatisch gespeichert und wiederverwendet, um schnellere Ergebnisse bei wiederholten Scans zu erhalten." },
                { Language.Italian, "Sistema Cache Intelligente: Le analisi vengono salvate automaticamente e riutilizzate per risultati più rapidi durante le scansioni ripetute." },
                { Language.Japanese, "スマートキャッシュシステム: 分析は自動的に保存され、繰り返しスキャンでより高速な結果が得られるように再利用されます。" }
            }},
            { "HISTORY_TITLE", new Dictionary<Language, string> {
                { Language.English, "Analysis History" },
                { Language.Spanish, "Historial de Análisis" },
                { Language.French, "Historique des Analyses" },
                { Language.German, "Analyseverlauf" },
                { Language.Italian, "Cronologia Analisi" },
                { Language.Japanese, "分析履歴" }
            }},
            { "HISTORY_COL_FOLDER", new Dictionary<Language, string> {
                { Language.English, "Folder" },
                { Language.Spanish, "Carpeta" },
                { Language.French, "Dossier" },
                { Language.German, "Ordner" },
                { Language.Italian, "Cartella" },
                { Language.Japanese, "フォルダー" }
            }},
            { "HISTORY_COL_DATE", new Dictionary<Language, string> {
                { Language.English, "Analysis Date" },
                { Language.Spanish, "Fecha del Análisis" },
                { Language.French, "Date d'Analyse" },
                { Language.German, "Analysedatum" },
                { Language.Italian, "Data Analisi" },
                { Language.Japanese, "分析日" }
            }},
            { "HISTORY_COL_FILES", new Dictionary<Language, string> {
                { Language.English, "📁 Files" },
                { Language.Spanish, "📁 Archivos" },
                { Language.French, "📁 Fichiers" },
                { Language.German, "📁 Dateien" },
                { Language.Italian, "📁 File" },
                { Language.Japanese, "📁 ファイル" }
            }},
            { "HISTORY_BTN_LOAD", new Dictionary<Language, string> {
                { Language.English, "📂 Load Analysis" },
                { Language.Spanish, "📂 Cargar Análisis" },
                { Language.French, "📂 Charger Analyse" },
                { Language.German, "📂 Analyse Laden" },
                { Language.Italian, "📂 Carica Analisi" },
                { Language.Japanese, "📂 分析を読み込む" }
            }},
            { "HISTORY_BTN_DELETE", new Dictionary<Language, string> {
                { Language.English, "🗑️ Delete" },
                { Language.Spanish, "🗑️ Eliminar" },
                { Language.French, "🗑️ Supprimer" },
                { Language.German, "🗑️ Löschen" },
                { Language.Italian, "🗑️ Elimina" },
                { Language.Japanese, "🗑️ 削除" }
            }},
            { "HISTORY_BTN_REFRESH", new Dictionary<Language, string> {
                { Language.English, "🔄 Refresh" },
                { Language.Spanish, "🔄 Refrescar" },
                { Language.French, "🔄 Actualiser" },
                { Language.German, "🔄 Aktualisieren" },
                { Language.Italian, "🔄 Aggiorna" },
                { Language.Japanese, "🔄 更新" }
            }},
            { "HISTORY_BTN_CLEAR", new Dictionary<Language, string> {
                { Language.English, "🧹 Clear All" },
                { Language.Spanish, "🧹 Limpiar Todo" },
                { Language.French, "🧹 Effacer Tout" },
                { Language.German, "🧹 Alles Löschen" },
                { Language.Italian, "🧹 Cancella Tutto" },
                { Language.Japanese, "🧹 すべてクリア" }
            }},
            { "HISTORY_BTN_CLOSE", new Dictionary<Language, string> {
                { Language.English, "☓ Close" },
                { Language.Spanish, "☓ Cerrar" },
                { Language.French, "☓ Fermer" },
                { Language.German, "☓ Schließen" },
                { Language.Italian, "☓ Chiudi" },
                { Language.Japanese, "☓ 閉じる" }
            }},
            { "HISTORY_NO_CACHE", new Dictionary<Language, string> {
                { Language.English, "ℹ️ No cached analyses" },
                { Language.Spanish, "ℹ️ No hay análisis guardados en caché" },
                { Language.French, "ℹ️ Aucune analyse mise en cache" },
                { Language.German, "ℹ️ Keine zwischengespeicherten Analysen" },
                { Language.Italian, "ℹ️ Nessuna analisi in cache" },
                { Language.Japanese, "ℹ️ キャッシュされた分析なし" }
            }},
            { "HISTORY_FOUND_FORMAT", new Dictionary<Language, string> {
                { Language.English, "✅ {0} analyses found in cache" },
                { Language.Spanish, "✅ {0} análisis encontrados en caché" },
                { Language.French, "✅ {0} analyses trouvées en cache" },
                { Language.German, "✅ {0} Analysen im Cache gefunden" },
                { Language.Italian, "✅ {0} analisi trovate in cache" },
                { Language.Japanese, "✅ キャッシュで{0}個の分析が見つかりました" }
            }},
            { "HISTORY_SELECT_TO_LOAD", new Dictionary<Language, string> {
                { Language.English, "Select an analysis to load." },
                { Language.Spanish, "Selecciona un análisis para cargar." },
                { Language.French, "Sélectionnez une analyse à charger." },
                { Language.German, "Wählen Sie eine zu ladende Analyse." },
                { Language.Italian, "Seleziona un'analisi da caricare." },
                { Language.Japanese, "読み込む分析を選択してください。" }
            }},
            { "HISTORY_ERROR_LOAD", new Dictionary<Language, string> {
                { Language.English, "Failed to load selected analysis." },
                { Language.Spanish, "No se pudo cargar el análisis seleccionado." },
                { Language.French, "Impossible de charger l'analyse sélectionnée." },
                { Language.German, "Ausgewählte Analyse konnte nicht geladen werden." },
                { Language.Italian, "Impossibile caricare l'analisi selezionata." },
                { Language.Japanese, "選択した分析を読み込むことができませんでした。" }
            }},
            { "HISTORY_SELECT_TO_DELETE", new Dictionary<Language, string> {
                { Language.English, "Select an analysis to delete." },
                { Language.Spanish, "Selecciona un análisis para eliminar." },
                { Language.French, "Sélectionnez une analyse à supprimer." },
                { Language.German, "Wählen Sie eine zu löschende Analyse." },
                { Language.Italian, "Seleziona un'analisi da eliminare." },
                { Language.Japanese, "削除する分析を選択してください。" }
            }},
            { "HISTORY_CONFIRM_DELETE", new Dictionary<Language, string> {
                { Language.English, "Are you sure you want to delete the cache for:\n\n{0}?" },
                { Language.Spanish, "¿Estás seguro de que deseas eliminar el caché para:\n\n{0}?" },
                { Language.French, "Êtes-vous sûr de vouloir supprimer le cache pour:\n\n{0}?" },
                { Language.German, "Sind Sie sicher, dass Sie den Cache für folgende Adresse löschen möchten:\n\n{0}?" },
                { Language.Italian, "Sei sicuro di voler eliminare la cache per:\n\n{0}?" },
                { Language.Japanese, "次のキャッシュを削除してもよろしいですか:\n\n{0}?" }
            }},
            { "HISTORY_CONFIRM_DELETE_TITLE", new Dictionary<Language, string> {
                { Language.English, "Confirm Deletion" },
                { Language.Spanish, "Confirmar eliminación" },
                { Language.French, "Confirmer la suppression" },
                { Language.German, "Löschung Bestätigen" },
                { Language.Italian, "Conferma Eliminazione" },
                { Language.Japanese, "削除を確認" }
            }},
            { "HISTORY_DELETED_SUCCESS", new Dictionary<Language, string> {
                { Language.English, "✅ Cache deleted successfully." },
                { Language.Spanish, "✅ Caché eliminado correctamente." },
                { Language.French, "✅ Cache supprimé avec succès." },
                { Language.German, "✅ Cache erfolgreich gelöscht." },
                { Language.Italian, "✅ Cache eliminato correttamente." },
                { Language.Japanese, "✅ キャッシュが正常に削除されました。" }
            }},
            { "HISTORY_CLEAR_WARNING", new Dictionary<Language, string> {
                { Language.English, "⚠️ Are you sure you want to delete ALL cache?\n\nThis action cannot be undone." },
                { Language.Spanish, "⚠️ ¿Estás seguro de que deseas eliminar TODO el caché?\n\nEsta acción no se puede deshacer." },
                { Language.French, "⚠️ Êtes-vous sûr de vouloir supprimer TOUT le cache?\n\nCette action ne peut pas être annulée." },
                { Language.German, "⚠️ Sind Sie sicher, dass Sie ALLE Cache löschen möchten?\n\nDiese Aktion kann nicht rückgängig gemacht werden." },
                { Language.Italian, "⚠️ Sei sicuro di voler eliminare TUTTA la cache?\n\nQuesta azione non può essere annullata." },
                { Language.Japanese, "⚠️ 全キャッシュを削除してもよろしいですか?\n\nこのアクションを元に戻すことはできません。" }
            }},
            { "HISTORY_CLEAR_TITLE", new Dictionary<Language, string> {
                { Language.English, "Clear All Cache" },
                { Language.Spanish, "Limpiar todo el caché" },
                { Language.French, "Effacer Tout le Cache" },
                { Language.German, "Alle Caches Löschen" },
                { Language.Italian, "Cancella Tutta la Cache" },
                { Language.Japanese, "すべてのキャッシュをクリア" }
            }},
            { "HISTORY_CLEARED_FORMAT", new Dictionary<Language, string> {
                { Language.English, "✅ {0} analyses deleted from cache." },
                { Language.Spanish, "✅ {0} análisis eliminados del caché." },
                { Language.French, "✅ {0} analyses supprimées du cache." },
                { Language.German, "✅ {0} Analysen aus dem Cache gelöscht." },
                { Language.Italian, "✅ {0} analisi eliminate dalla cache." },
                { Language.Japanese, "✅ キャッシュから{0}個の分析が削除されました。" }
            }},
            { "TITLE_INFORMATION", new Dictionary<Language, string> {
                { Language.English, "Information" },
                { Language.Spanish, "Información" },
                { Language.French, "Information" },
                { Language.German, "Information" },
                { Language.Italian, "Informazione" },
                { Language.Japanese, "情報" }
            }},
            { "TITLE_ERROR", new Dictionary<Language, string> {
                { Language.English, "Error" },
                { Language.Spanish, "Error" },
                { Language.French, "Erreur" },
                { Language.German, "Fehler" },
                { Language.Italian, "Errore" },
                { Language.Japanese, "エラー" }
            }},
            { "TITLE_SUCCESS", new Dictionary<Language, string> {
                { Language.English, "Success" },
                { Language.Spanish, "Éxito" },
                { Language.French, "Succès" },
                { Language.German, "Erfolg" },
                { Language.Italian, "Successo" },
                { Language.Japanese, "成功" }
            }},
            { "CACHE_CHECK_DIALOG_TITLE", new Dictionary<Language, string> {
                { Language.English, "Cached Analysis" },
                { Language.Spanish, "Análisis en Caché" },
                { Language.French, "Analyse en Cache" },
                { Language.German, "Gecachte Analyse" },
                { Language.Italian, "Analisi in Cache" },
                { Language.Japanese, "キャッシュされた分析" }
            }},
            { "CACHE_CHANGED_MESSAGE", new Dictionary<Language, string> {
                { Language.English, "Previous analysis found from {0}\n\n⚠ The folder has changed since the last analysis.\n\nDo you want to reuse the previous analysis or perform a new one?" },
                { Language.Spanish, "Se encontró análisis anterior del {0}\n\n⚠ La carpeta ha cambiado desde el último análisis.\n\n¿Deseas reutilizar el análisis anterior o hacer uno nuevo?" },
                { Language.French, "Analyse antérieure trouvée du {0}\n\n⚠ Le dossier a changé depuis la dernière analyse.\n\nVoulez-vous réutiliser l'analyse antérieure ou en effectuer une nouvelle?" },
                { Language.German, "Frühere Analyse vom {0} gefunden\n\n⚠ Der Ordner hat sich seit der letzten Analyse geändert.\n\nMöchten Sie die vorherige Analyse wiederverwenden oder eine neue durchführen?" },
                { Language.Italian, "Analisi precedente trovata del {0}\n\n⚠ La cartella è cambiata dall'ultima analisi.\n\nVuoi riutilizzare l'analisi precedente o eseguire una nuova?" },
                { Language.Japanese, "前回の分析が見つかりました{0}\n\n⚠ フォルダは最後の分析以降に変更されました。\n\n前の分析を再利用するか、新しい分析を実行しますか?" }
            }},
            { "CACHE_UNCHANGED_MESSAGE", new Dictionary<Language, string> {
                { Language.English, "Previous analysis found from {0}\n\n✅ The folder has not changed.\n\nDo you want to reuse this analysis (faster) or perform a new one?" },
                { Language.Spanish, "Se encontró análisis anterior del {0}\n\n✅ La carpeta no ha cambiado.\n\n¿Deseas reutilizar este análisis (más rápido) o hacer uno nuevo?" },
                { Language.French, "Analyse antérieure trouvée du {0}\n\n✅ Le dossier n'a pas changé.\n\nVoulez-vous réutiliser cette analyse (plus rapide) ou en effectuer une nouvelle?" },
                { Language.German, "Frühere Analyse vom {0} gefunden\n\n✅ Der Ordner hat sich nicht geändert.\n\nMöchten Sie diese Analyse wiederverwenden (schneller) oder eine neue durchführen?" },
                { Language.Italian, "Analisi precedente trovata del {0}\n\n✅ La cartella non è cambiata.\n\nVuoi riutilizzare questa analisi (più veloce) o eseguire una nuova?" },
                { Language.Japanese, "前回の分析が見つかりました{0}\n\n✅ フォルダは変更されていません。\n\nこの分析を再利用する（高速）または新しい分析を実行しますか?" }
            }},
            { "CACHE_LOADED_STATUS", new Dictionary<Language, string> {
                { Language.English, "✅ Analysis loaded from cache ({0:dd/MM/yyyy HH:mm})" },
                { Language.Spanish, "✅ Análisis cargado desde caché ({0:dd/MM/yyyy HH:mm})" },
                { Language.French, "✅ Analyse chargée à partir du cache ({0:dd/MM/yyyy HH:mm})" },
                { Language.German, "✅ Analyse aus Cache geladen ({0:dd/MM/yyyy HH:mm})" },
                { Language.Italian, "✅ Analisi caricata dalla cache ({0:dd/MM/yyyy HH:mm})" },
                { Language.Japanese, "✅ キャッシュから分析を読み込みました ({0:dd/MM/yyyy HH:mm})" }
            }},
            { "CACHE_BTN_USE_PREVIOUS", new Dictionary<Language, string> {
                { Language.English, "Use previous analysis" },
                { Language.Spanish, "Usar análisis anterior" },
                { Language.French, "Utiliser l'analyse antérieure" },
                { Language.German, "Vorherige Analyse verwenden" },
                { Language.Italian, "Usa analisi precedente" },
                { Language.Japanese, "前の分析を使用" }
            }},
            { "CACHE_BTN_REDO", new Dictionary<Language, string> {
                { Language.English, "Redo analysis" },
                { Language.Spanish, "Rehacer análisis" },
                { Language.French, "Refaire l'analyse" },
                { Language.German, "Analyse wiederholen" },
                { Language.Italian, "Ripeti analisi" },
                { Language.Japanese, "分析をやり直す" }
            }},
            { "CACHE_BTN_CANCEL", new Dictionary<Language, string> {
                { Language.English, "Cancel" },
                { Language.Spanish, "Cancelar" },
                { Language.French, "Annuler" },
                { Language.German, "Abbrechen" },
                { Language.Italian, "Annulla" },
                { Language.Japanese, "キャンセル" }
            }},
            { "HELP_TITLE", new Dictionary<Language, string> {
                { Language.English, "KiloFilter Help - How to Use" },
                { Language.Spanish, "Ayuda de KiloFilter - Cómo Usar" },
                { Language.French, "Aide KiloFilter - Comment Utiliser" },
                { Language.German, "KiloFilter Hilfe - Anleitung" },
                { Language.Italian, "Aiuto KiloFilter - Come Usare" },
                { Language.Japanese, "KiloFilter ヘルプ - 使い方" }
            }},
            // Búsqueda y Filtrado Avanzado
            { "SEARCH_FILTER", new Dictionary<Language, string> {
                { Language.English, "🔍 Search & Filter" },
                { Language.Spanish, "🔍 Búsqueda y Filtrado" },
                { Language.French, "🔍 Recherche et Filtrage" },
                { Language.German, "🔍 Suche und Filterung" },
                { Language.Italian, "🔍 Ricerca e Filtro" },
                { Language.Japanese, "🔍 検索とフィルター" }
            }},
            { "FILTER_BY_NAME", new Dictionary<Language, string> {
                { Language.English, "Filter by filename:" },
                { Language.Spanish, "Filtrar por nombre:" },
                { Language.French, "Filtrer par nom:" },
                { Language.German, "Nach Name filtern:" },
                { Language.Italian, "Filtrare per nome:" },
                { Language.Japanese, "ファイル名でフィルター:" }
            }},
            { "FILTER_BY_SIZE", new Dictionary<Language, string> {
                { Language.English, "Min size:" },
                { Language.Spanish, "Tamaño mínimo:" },
                { Language.French, "Taille min:" },
                { Language.German, "Mindestgröße:" },
                { Language.Italian, "Dimensione minima:" },
                { Language.Japanese, "最小サイズ:" }
            }},
            { "FILTER_MAX_SIZE", new Dictionary<Language, string> {
                { Language.English, "Max size:" },
                { Language.Spanish, "Tamaño máximo:" },
                { Language.French, "Taille max:" },
                { Language.German, "Maximale Größe:" },
                { Language.Italian, "Dimensione massima:" },
                { Language.Japanese, "最大サイズ:" }
            }},
            { "SMART_DELETE", new Dictionary<Language, string> {
                { Language.English, "☠ Smart Delete Duplicates" },
                { Language.Spanish, "☠ Eliminar Duplicados Inteligentemente" },
                { Language.French, "☠ Supprimer les Doublons Intelligemment" },
                { Language.German, "☠ Duplikate Intelligent Löschen" },
                { Language.Italian, "☠Elimina i Duplicati Intelligentemente" },
                { Language.Japanese, "☠ スマート削除" }
            }},
            { "KEEP_NEWEST", new Dictionary<Language, string> {
                { Language.English, "Keep newest" },
                { Language.Spanish, "Guardar más reciente" },
                { Language.French, "Garder le plus récent" },
                { Language.German, "Neuste behalten" },
                { Language.Italian, "Mantieni il più recente" },
                { Language.Japanese, "最新のものを保持" }
            }},
            { "KEEP_OLDEST", new Dictionary<Language, string> {
                { Language.English, "Keep oldest" },
                { Language.Spanish, "Guardar más antiguo" },
                { Language.French, "Garder le plus ancien" },
                { Language.German, "Älteste behalten" },
                { Language.Italian, "Mantieni il più vecchio" },
                { Language.Japanese, "最古のものを保持" }
            }},
            { "KEEP_SMALLEST", new Dictionary<Language, string> {
                { Language.English, "Keep smallest" },
                { Language.Spanish, "Guardar el más pequeño" },
                { Language.French, "Garder le plus petit" },
                { Language.German, "Kleinste behalten" },
                { Language.Italian, "Mantieni il più piccolo" },
                { Language.Japanese, "最小のものを保持" }
            }},
            { "DELETE_PREVIEW", new Dictionary<Language, string> {
                { Language.English, "Preview delete:" },
                { Language.Spanish, "Vista previa de eliminación:" },
                { Language.French, "Aperçu de la suppression:" },
                { Language.German, "Vorschau löschen:" },
                { Language.Italian, "Anteprima elimina:" },
                { Language.Japanese, "削除プレビュー:" }
            }},
            { "DELETE_CONFIRM", new Dictionary<Language, string> {
                { Language.English, "⚠️  Delete {0} files? This cannot be undone!" },
                { Language.Spanish, "⚠️  ¿Eliminar {0} archivos? ¡Esto no se puede deshacer!" },
                { Language.French, "⚠️  Supprimer {0} fichiers? Cela ne peut pas être annulé!" },
                { Language.German, "⚠️  {0} Dateien löschen? Dies kann nicht rückgängig gemacht werden!" },
                { Language.Italian, "⚠️  Eliminare {0} file? Questo non può essere annullato!" },
                { Language.Japanese, "⚠️  {0}ファイルを削除しますか? これは取り消せません!" }
            }},
            { "DELETE_SUCCESS", new Dictionary<Language, string> {
                { Language.English, "✅ Deleted {0} duplicate files ({1})" },
                { Language.Spanish, "✅ Eliminados {0} archivos duplicados ({1})" },
                { Language.French, "✅ {0} fichiers en double supprimés ({1})" },
                { Language.German, "✅ {0} Duplikatdateien gelöscht ({1})" },
                { Language.Italian, "✅ Eliminati {0} file duplicati ({1})" },
                { Language.Japanese, "✅ {0}個の重複ファイルを削除しました ({1})" }
            }},
            { "NO_DUPLICATES_FOUND", new Dictionary<Language, string> {
                { Language.English, "No duplicates match the selected criteria" },
                { Language.Spanish, "No hay duplicados que coincidan con los criterios seleccionados" },
                { Language.French, "Aucun doublon ne correspond aux critères sélectionnés" },
                { Language.German, "Keine Duplikate entsprechen den ausgewählten Kriterien" },
                { Language.Italian, "Nessun duplicato corrisponde ai criteri selezionati" },
                { Language.Japanese, "選択した条件に一致する重複ファイルはありません" }
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
