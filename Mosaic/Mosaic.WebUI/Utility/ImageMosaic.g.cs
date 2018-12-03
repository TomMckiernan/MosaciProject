// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: ImageMosaic.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from ImageMosaic.proto</summary>
public static partial class ImageMosaicReflection {

  #region Descriptor
  /// <summary>File descriptor for ImageMosaic.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static ImageMosaicReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "ChFJbWFnZU1vc2FpYy5wcm90bxoUSW1hZ2VGaWxlSW5kZXgucHJvdG8icwoS",
          "SW1hZ2VNb3NhaWNSZXF1ZXN0EgoKAklkGAEgASgJEicKBVRpbGVzGAIgAygL",
          "MhguSW1hZ2VGaWxlSW5kZXhTdHJ1Y3R1cmUSKAoGTWFzdGVyGAMgASgLMhgu",
          "SW1hZ2VGaWxlSW5kZXhTdHJ1Y3R1cmUiNgoTSW1hZ2VNb3NhaWNSZXNwb25z",
          "ZRIQCghsb2NhdGlvbhgBIAEoCRINCgVFcnJvchgCIAEoCWIGcHJvdG8z"));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { global::ImageFileIndexReflection.Descriptor, },
        new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::ImageMosaicRequest), global::ImageMosaicRequest.Parser, new[]{ "Id", "Tiles", "Master" }, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::ImageMosaicResponse), global::ImageMosaicResponse.Parser, new[]{ "Location", "Error" }, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class ImageMosaicRequest : pb::IMessage<ImageMosaicRequest> {
  private static readonly pb::MessageParser<ImageMosaicRequest> _parser = new pb::MessageParser<ImageMosaicRequest>(() => new ImageMosaicRequest());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<ImageMosaicRequest> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::ImageMosaicReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ImageMosaicRequest() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ImageMosaicRequest(ImageMosaicRequest other) : this() {
    id_ = other.id_;
    tiles_ = other.tiles_.Clone();
    master_ = other.master_ != null ? other.master_.Clone() : null;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ImageMosaicRequest Clone() {
    return new ImageMosaicRequest(this);
  }

  /// <summary>Field number for the "Id" field.</summary>
  public const int IdFieldNumber = 1;
  private string id_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Id {
    get { return id_; }
    set {
      id_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "Tiles" field.</summary>
  public const int TilesFieldNumber = 2;
  private static readonly pb::FieldCodec<global::ImageFileIndexStructure> _repeated_tiles_codec
      = pb::FieldCodec.ForMessage(18, global::ImageFileIndexStructure.Parser);
  private readonly pbc::RepeatedField<global::ImageFileIndexStructure> tiles_ = new pbc::RepeatedField<global::ImageFileIndexStructure>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pbc::RepeatedField<global::ImageFileIndexStructure> Tiles {
    get { return tiles_; }
  }

  /// <summary>Field number for the "Master" field.</summary>
  public const int MasterFieldNumber = 3;
  private global::ImageFileIndexStructure master_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public global::ImageFileIndexStructure Master {
    get { return master_; }
    set {
      master_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as ImageMosaicRequest);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(ImageMosaicRequest other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Id != other.Id) return false;
    if(!tiles_.Equals(other.tiles_)) return false;
    if (!object.Equals(Master, other.Master)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Id.Length != 0) hash ^= Id.GetHashCode();
    hash ^= tiles_.GetHashCode();
    if (master_ != null) hash ^= Master.GetHashCode();
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
    tiles_.WriteTo(output, _repeated_tiles_codec);
    if (master_ != null) {
      output.WriteRawTag(26);
      output.WriteMessage(Master);
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
    size += tiles_.CalculateSize(_repeated_tiles_codec);
    if (master_ != null) {
      size += 1 + pb::CodedOutputStream.ComputeMessageSize(Master);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(ImageMosaicRequest other) {
    if (other == null) {
      return;
    }
    if (other.Id.Length != 0) {
      Id = other.Id;
    }
    tiles_.Add(other.tiles_);
    if (other.master_ != null) {
      if (master_ == null) {
        master_ = new global::ImageFileIndexStructure();
      }
      Master.MergeFrom(other.Master);
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
          tiles_.AddEntriesFrom(input, _repeated_tiles_codec);
          break;
        }
        case 26: {
          if (master_ == null) {
            master_ = new global::ImageFileIndexStructure();
          }
          input.ReadMessage(master_);
          break;
        }
      }
    }
  }

}

public sealed partial class ImageMosaicResponse : pb::IMessage<ImageMosaicResponse> {
  private static readonly pb::MessageParser<ImageMosaicResponse> _parser = new pb::MessageParser<ImageMosaicResponse>(() => new ImageMosaicResponse());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<ImageMosaicResponse> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::ImageMosaicReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ImageMosaicResponse() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ImageMosaicResponse(ImageMosaicResponse other) : this() {
    location_ = other.location_;
    error_ = other.error_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ImageMosaicResponse Clone() {
    return new ImageMosaicResponse(this);
  }

  /// <summary>Field number for the "location" field.</summary>
  public const int LocationFieldNumber = 1;
  private string location_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Location {
    get { return location_; }
    set {
      location_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
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
    return Equals(other as ImageMosaicResponse);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(ImageMosaicResponse other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Location != other.Location) return false;
    if (Error != other.Error) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Location.Length != 0) hash ^= Location.GetHashCode();
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
    if (Location.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(Location);
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
    if (Location.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Location);
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
  public void MergeFrom(ImageMosaicResponse other) {
    if (other == null) {
      return;
    }
    if (other.Location.Length != 0) {
      Location = other.Location;
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
          Location = input.ReadString();
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