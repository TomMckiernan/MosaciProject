// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: ImageFileIndex.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from ImageFileIndex.proto</summary>
public static partial class ImageFileIndexReflection {

  #region Descriptor
  /// <summary>File descriptor for ImageFileIndex.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static ImageFileIndexReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "ChRJbWFnZUZpbGVJbmRleC5wcm90byIwChVJbWFnZUZpbGVJbmRleFJlcXVl",
          "c3QSFwoPSW5kZXhlZExvY2F0aW9uGAEgASgJIlAKFkltYWdlRmlsZUluZGV4",
          "UmVzcG9uc2USJwoFRmlsZXMYASADKAsyGC5JbWFnZUZpbGVJbmRleFN0cnVj",
          "dHVyZRINCgVFcnJvchgCIAEoCSJyChdJbWFnZUZpbGVJbmRleFN0cnVjdHVy",
          "ZRIKCgJpZBgBIAEoCRIQCghGaWxlUGF0aBgCIAEoCRIQCghGaWxlTmFtZRgD",
          "IAEoCRIQCghNZXRhZGF0YRgEIAEoCRIVCg1MYXN0V3JpdGVUaW1lGAUgASgJ",
          "IkIKH0ltYWdlRmlsZUluZGV4U3RydWN0dXJlUmVzcG9uc2USEAoIRmlsZVBh",
          "dGgYASABKAkSDQoFRXJyb3IYAiABKAliBnByb3RvMw=="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::ImageFileIndexRequest), global::ImageFileIndexRequest.Parser, new[]{ "IndexedLocation" }, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::ImageFileIndexResponse), global::ImageFileIndexResponse.Parser, new[]{ "Files", "Error" }, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::ImageFileIndexStructure), global::ImageFileIndexStructure.Parser, new[]{ "Id", "FilePath", "FileName", "Metadata", "LastWriteTime" }, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::ImageFileIndexStructureResponse), global::ImageFileIndexStructureResponse.Parser, new[]{ "FilePath", "Error" }, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class ImageFileIndexRequest : pb::IMessage<ImageFileIndexRequest> {
  private static readonly pb::MessageParser<ImageFileIndexRequest> _parser = new pb::MessageParser<ImageFileIndexRequest>(() => new ImageFileIndexRequest());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<ImageFileIndexRequest> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::ImageFileIndexReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ImageFileIndexRequest() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ImageFileIndexRequest(ImageFileIndexRequest other) : this() {
    indexedLocation_ = other.indexedLocation_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ImageFileIndexRequest Clone() {
    return new ImageFileIndexRequest(this);
  }

  /// <summary>Field number for the "IndexedLocation" field.</summary>
  public const int IndexedLocationFieldNumber = 1;
  private string indexedLocation_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string IndexedLocation {
    get { return indexedLocation_; }
    set {
      indexedLocation_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as ImageFileIndexRequest);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(ImageFileIndexRequest other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (IndexedLocation != other.IndexedLocation) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (IndexedLocation.Length != 0) hash ^= IndexedLocation.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (IndexedLocation.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(IndexedLocation);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (IndexedLocation.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(IndexedLocation);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(ImageFileIndexRequest other) {
    if (other == null) {
      return;
    }
    if (other.IndexedLocation.Length != 0) {
      IndexedLocation = other.IndexedLocation;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          IndexedLocation = input.ReadString();
          break;
        }
      }
    }
  }

}

public sealed partial class ImageFileIndexResponse : pb::IMessage<ImageFileIndexResponse> {
  private static readonly pb::MessageParser<ImageFileIndexResponse> _parser = new pb::MessageParser<ImageFileIndexResponse>(() => new ImageFileIndexResponse());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<ImageFileIndexResponse> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::ImageFileIndexReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ImageFileIndexResponse() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ImageFileIndexResponse(ImageFileIndexResponse other) : this() {
    files_ = other.files_.Clone();
    error_ = other.error_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ImageFileIndexResponse Clone() {
    return new ImageFileIndexResponse(this);
  }

  /// <summary>Field number for the "Files" field.</summary>
  public const int FilesFieldNumber = 1;
  private static readonly pb::FieldCodec<global::ImageFileIndexStructure> _repeated_files_codec
      = pb::FieldCodec.ForMessage(10, global::ImageFileIndexStructure.Parser);
  private readonly pbc::RepeatedField<global::ImageFileIndexStructure> files_ = new pbc::RepeatedField<global::ImageFileIndexStructure>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pbc::RepeatedField<global::ImageFileIndexStructure> Files {
    get { return files_; }
  }

  /// <summary>Field number for the "Error" field.</summary>
  public const int ErrorFieldNumber = 2;
  private string error_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Error {
    get { return error_; }
    set {
      error_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as ImageFileIndexResponse);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(ImageFileIndexResponse other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if(!files_.Equals(other.files_)) return false;
    if (Error != other.Error) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    hash ^= files_.GetHashCode();
    if (Error.Length != 0) hash ^= Error.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    files_.WriteTo(output, _repeated_files_codec);
    if (Error.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Error);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    size += files_.CalculateSize(_repeated_files_codec);
    if (Error.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Error);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(ImageFileIndexResponse other) {
    if (other == null) {
      return;
    }
    files_.Add(other.files_);
    if (other.Error.Length != 0) {
      Error = other.Error;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          files_.AddEntriesFrom(input, _repeated_files_codec);
          break;
        }
        case 18: {
          Error = input.ReadString();
          break;
        }
      }
    }
  }

}

public sealed partial class ImageFileIndexStructure : pb::IMessage<ImageFileIndexStructure> {
  private static readonly pb::MessageParser<ImageFileIndexStructure> _parser = new pb::MessageParser<ImageFileIndexStructure>(() => new ImageFileIndexStructure());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<ImageFileIndexStructure> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::ImageFileIndexReflection.Descriptor.MessageTypes[2]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ImageFileIndexStructure() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ImageFileIndexStructure(ImageFileIndexStructure other) : this() {
    id_ = other.id_;
    filePath_ = other.filePath_;
    fileName_ = other.fileName_;
    metadata_ = other.metadata_;
    lastWriteTime_ = other.lastWriteTime_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ImageFileIndexStructure Clone() {
    return new ImageFileIndexStructure(this);
  }

  /// <summary>Field number for the "id" field.</summary>
  public const int IdFieldNumber = 1;
  private string id_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Id {
    get { return id_; }
    set {
      id_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "FilePath" field.</summary>
  public const int FilePathFieldNumber = 2;
  private string filePath_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string FilePath {
    get { return filePath_; }
    set {
      filePath_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "FileName" field.</summary>
  public const int FileNameFieldNumber = 3;
  private string fileName_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string FileName {
    get { return fileName_; }
    set {
      fileName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "Metadata" field.</summary>
  public const int MetadataFieldNumber = 4;
  private string metadata_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Metadata {
    get { return metadata_; }
    set {
      metadata_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "LastWriteTime" field.</summary>
  public const int LastWriteTimeFieldNumber = 5;
  private string lastWriteTime_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string LastWriteTime {
    get { return lastWriteTime_; }
    set {
      lastWriteTime_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as ImageFileIndexStructure);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(ImageFileIndexStructure other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Id != other.Id) return false;
    if (FilePath != other.FilePath) return false;
    if (FileName != other.FileName) return false;
    if (Metadata != other.Metadata) return false;
    if (LastWriteTime != other.LastWriteTime) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Id.Length != 0) hash ^= Id.GetHashCode();
    if (FilePath.Length != 0) hash ^= FilePath.GetHashCode();
    if (FileName.Length != 0) hash ^= FileName.GetHashCode();
    if (Metadata.Length != 0) hash ^= Metadata.GetHashCode();
    if (LastWriteTime.Length != 0) hash ^= LastWriteTime.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (Id.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(Id);
    }
    if (FilePath.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(FilePath);
    }
    if (FileName.Length != 0) {
      output.WriteRawTag(26);
      output.WriteString(FileName);
    }
    if (Metadata.Length != 0) {
      output.WriteRawTag(34);
      output.WriteString(Metadata);
    }
    if (LastWriteTime.Length != 0) {
      output.WriteRawTag(42);
      output.WriteString(LastWriteTime);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (Id.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Id);
    }
    if (FilePath.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(FilePath);
    }
    if (FileName.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(FileName);
    }
    if (Metadata.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Metadata);
    }
    if (LastWriteTime.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(LastWriteTime);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(ImageFileIndexStructure other) {
    if (other == null) {
      return;
    }
    if (other.Id.Length != 0) {
      Id = other.Id;
    }
    if (other.FilePath.Length != 0) {
      FilePath = other.FilePath;
    }
    if (other.FileName.Length != 0) {
      FileName = other.FileName;
    }
    if (other.Metadata.Length != 0) {
      Metadata = other.Metadata;
    }
    if (other.LastWriteTime.Length != 0) {
      LastWriteTime = other.LastWriteTime;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          Id = input.ReadString();
          break;
        }
        case 18: {
          FilePath = input.ReadString();
          break;
        }
        case 26: {
          FileName = input.ReadString();
          break;
        }
        case 34: {
          Metadata = input.ReadString();
          break;
        }
        case 42: {
          LastWriteTime = input.ReadString();
          break;
        }
      }
    }
  }

}

public sealed partial class ImageFileIndexStructureResponse : pb::IMessage<ImageFileIndexStructureResponse> {
  private static readonly pb::MessageParser<ImageFileIndexStructureResponse> _parser = new pb::MessageParser<ImageFileIndexStructureResponse>(() => new ImageFileIndexStructureResponse());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<ImageFileIndexStructureResponse> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::ImageFileIndexReflection.Descriptor.MessageTypes[3]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ImageFileIndexStructureResponse() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ImageFileIndexStructureResponse(ImageFileIndexStructureResponse other) : this() {
    filePath_ = other.filePath_;
    error_ = other.error_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ImageFileIndexStructureResponse Clone() {
    return new ImageFileIndexStructureResponse(this);
  }

  /// <summary>Field number for the "FilePath" field.</summary>
  public const int FilePathFieldNumber = 1;
  private string filePath_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string FilePath {
    get { return filePath_; }
    set {
      filePath_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "Error" field.</summary>
  public const int ErrorFieldNumber = 2;
  private string error_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Error {
    get { return error_; }
    set {
      error_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as ImageFileIndexStructureResponse);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(ImageFileIndexStructureResponse other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (FilePath != other.FilePath) return false;
    if (Error != other.Error) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (FilePath.Length != 0) hash ^= FilePath.GetHashCode();
    if (Error.Length != 0) hash ^= Error.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (FilePath.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(FilePath);
    }
    if (Error.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Error);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (FilePath.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(FilePath);
    }
    if (Error.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Error);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(ImageFileIndexStructureResponse other) {
    if (other == null) {
      return;
    }
    if (other.FilePath.Length != 0) {
      FilePath = other.FilePath;
    }
    if (other.Error.Length != 0) {
      Error = other.Error;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          FilePath = input.ReadString();
          break;
        }
        case 18: {
          Error = input.ReadString();
          break;
        }
      }
    }
  }

}

#endregion


#endregion Designer generated code